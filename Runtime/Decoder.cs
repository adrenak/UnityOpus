using UnityEngine;
using System;

namespace Adrenak.UnityOpus {
    /// <summary>
    /// Represents an Opus decoder for decoding Opus-encoded data to PCM.
    /// </summary>
    public class Decoder : IDisposable {
        /// <summary>
        /// The maximum packet duration supported by the Opus decoder.
        /// </summary>
        public const int maximumPacketDuration = OpusLib.maximumPacketDuration;

        private IntPtr decoder;
        private readonly NumChannels channels;
        private readonly float[] softclipMem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Decoder"/> class.
        /// </summary>
        /// <param name="samplingFrequency">The sampling frequency of the input data.</param>
        /// <param name="channels">The number of audio channels.</param>
        public Decoder(SamplingFrequency samplingFrequency, NumChannels channels) {
            ErrorCode error;
            this.channels = channels;
            decoder = OpusLib.OpusDecoderCreate(samplingFrequency, channels, out error);
            if (error != ErrorCode.OK) {
                Debug.LogError("[UnityOpus] Failed to create Decoder. Error code is " + error.ToString());
                decoder = IntPtr.Zero;
            }
            softclipMem = new float[(int)channels];
        }

        /// <summary>
        /// Decodes the provided Opus data into PCM float format.
        /// </summary>
        /// <param name="data">The Opus-encoded byte data.</param>
        /// <param name="dataLength">The length of the input data.</param>
        /// <param name="pcm">Output float array for PCM data.</param>
        /// <param name="decodeFec">Flag to enable forward error correction (FEC).</param>
        /// <returns>The length of the decoded PCM data.</returns>
        public int Decode(byte[] data, int dataLength, float[] pcm, int decodeFec = 0) {
            if (decoder == IntPtr.Zero) {
                return 0;
            }
            var decodedLength = OpusLib.OpusDecodeFloat(
                decoder,
                data,
                dataLength,
                pcm,
                pcm.Length / (int)channels,
                decodeFec);
            OpusLib.OpusPcmSoftClip(
                pcm,
                decodedLength / (int)channels,
                channels,
                softclipMem);
            return decodedLength;
        }

        #region IDisposable Support
        private bool disposed = false; // To detect duplicate calls

        /// <summary>
        /// Releases the unmanaged resources used by the decoder.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (decoder == IntPtr.Zero) {
                    return;
                }
                OpusLib.OpusDecoderDestroy(decoder);
                decoder = IntPtr.Zero;

                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer for <see cref="Decoder"/>.
        /// </summary>
        ~Decoder() {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of the decoder and suppresses finalization.
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
