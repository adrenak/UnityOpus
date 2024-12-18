using System;

namespace Adrenak.UnityOpus {
    /// <summary>
    /// Represents an Opus encoder for encoding PCM data.
    /// </summary>
    public class Encoder : IDisposable {
        private int bitrate;
        /// <summary>
        /// Gets or sets the bitrate of the encoder.
        /// NOTE: get only returns the value that's been
        /// set by you before.
        /// </summary>
        public int Bitrate {
            get { return bitrate; }
            set {
                OpusLib.OpusEncoderSetBitrate(encoder, value);
                bitrate = value;
            }
        }

        private int complexity;
        /// <summary>
        /// Gets or sets the complexity level of the encoder.
        /// NOTE: get only returns the value that's been
        /// set by you before.
        /// </summary>
        public int Complexity {
            get {
                return complexity;
            }
            set {
                OpusLib.OpusEncoderSetComplexity(encoder, value);
                complexity = value;
            }
        }

        private OpusSignal signal;
        /// <summary>
        /// Gets or sets the signal type (e.g., voice, music) for the encoder.
        /// NOTE: get only returns the value that's been
        /// set by you before.
        /// </summary>
        public OpusSignal Signal {
            get { return signal; }
            set {
                OpusLib.OpusEncoderSetSignal(encoder, value);
                signal = value;
            }
        }

        private IntPtr encoder;
        private NumChannels channels;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encoder"/> class.
        /// </summary>
        /// <param name="samplingFrequency">The sampling frequency of the input data.</param>
        /// <param name="channels">The number of audio channels.</param>
        /// <param name="application">The Opus application type (e.g., VoIP, Audio).</param>
        public Encoder(SamplingFrequency samplingFrequency, NumChannels channels, OpusApplication application) {
            this.channels = channels;
            ErrorCode error;
            encoder = OpusLib.OpusEncoderCreate(
                samplingFrequency,
                channels,
                application,
                out error);
            if (error != ErrorCode.OK) {
                UnityEngine.Debug.LogError("[UnityOpus] Failed to init encoder. Error code: " + error.ToString());
                encoder = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Encodes the provided PCM float data to Opus format.
        /// </summary>
        /// <param name="pcm">Input PCM float array.</param>
        /// <param name="output">Output byte array for encoded data.</param>
        /// <returns>The size of the encoded data in bytes.</returns>
        public int Encode(float[] pcm, byte[] output) {
            if (encoder == IntPtr.Zero) {
                return 0;
            }
            return OpusLib.OpusEncodeFloat(
                encoder,
                pcm,
                pcm.Length / (int)channels,
                output,
                output.Length
            );
        }

        #region IDisposable Support
        private bool disposed = false; // To detect duplicate calls

        /// <summary>
        /// Releases the unmanaged resources used by the encoder.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (encoder == IntPtr.Zero) {
                    return;
                }
                OpusLib.OpusEncoderDestroy(encoder);
                encoder = IntPtr.Zero;

                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer for <see cref="Encoder"/>.
        /// </summary>
        ~Encoder() {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of the encoder and suppresses finalization.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}