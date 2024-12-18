using System;
using System.Runtime.InteropServices;

namespace Adrenak.UnityOpus {
    public class OpusLib {
        public const int maximumPacketDuration = 5760;

#if UNITY_ANDROID
        const string dllName = "unityopus";
#else
        const string dllName = "UnityOpus";
#endif

        /// <summary>
        /// Creates a new Opus encoder instance.
        /// </summary>
        /// <param name="samplingFrequency">The sampling frequency to be used by the encoder (e.g., 48kHz).</param>
        /// <param name="channels">The number of audio channels (mono or stereo).</param>
        /// <param name="application">The target application for the encoder (e.g., VoIP, Audio, or LowDelay).</param>
        /// <param name="error">Outputs an error code indicating success or failure.</param>
        /// <returns>A pointer to the newly created Opus encoder instance.</returns>
        [DllImport(dllName)]
        public static extern IntPtr OpusEncoderCreate(
            SamplingFrequency samplingFrequency,
            NumChannels channels,
            OpusApplication application,
            out ErrorCode error);

        /// <summary>
        /// Encodes audio samples into Opus format.
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance.</param>
        /// <param name="pcm">The PCM audio samples to encode (16-bit signed integers).</param>
        /// <param name="frameSize">The number of samples per frame.</param>
        /// <param name="data">The output buffer where the encoded data will be stored.</param>
        /// <param name="maxDataBytes">The maximum size of the output buffer in bytes.</param>
        /// <returns>The length of the encoded data in bytes, or an error code if failed.</returns>
        [DllImport(dllName)]
        public static extern int OpusEncode(
            IntPtr encoder,
            short[] pcm,
            int frameSize,
            byte[] data,
            int maxDataBytes);

        /// <summary>
        /// Encodes audio samples in floating-point format into Opus format.
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance.</param>
        /// <param name="pcm">The PCM audio samples to encode (32-bit floating-point).</param>
        /// <param name="frameSize">The number of samples per frame.</param>
        /// <param name="data">The output buffer where the encoded data will be stored.</param>
        /// <param name="maxDataBytes">The maximum size of the output buffer in bytes.</param>
        /// <returns>The length of the encoded data in bytes, or an error code if failed.</returns>
        [DllImport(dllName)]
        public static extern int OpusEncodeFloat(
            IntPtr encoder,
            float[] pcm,
            int frameSize,
            byte[] data,
            int maxDataBytes);

        /// <summary>
        /// Destroys an Opus encoder instance, freeing its allocated resources.
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance to be destroyed.</param>
        [DllImport(dllName)]
        public static extern void OpusEncoderDestroy(
            IntPtr encoder);

        /// <summary>
        /// Sets the bitrate of the Opus encoder.
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance.</param>
        /// <param name="bitrate">The desired bitrate in bits per second (e.g., 64000 for 64kbps).</param>
        /// <returns>Returns 0 on success, or an error code otherwise.</returns>
        [DllImport(dllName)]
        public static extern int OpusEncoderSetBitrate(
            IntPtr encoder,
            int bitrate);

        /// <summary>
        /// Sets the complexity of the Opus encoder.
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance.</param>
        /// <param name="complexity">The complexity level (0-10, where 10 is the highest quality).</param>
        /// <returns>Returns 0 on success, or an error code otherwise.</returns>
        [DllImport(dllName)]
        public static extern int OpusEncoderSetComplexity(
            IntPtr encoder,
            int complexity);

        /// <summary>
        /// Configures the encoder signal type (e.g., voice or music).
        /// </summary>
        /// <param name="encoder">A pointer to the Opus encoder instance.</param>
        /// <param name="signal">The signal type, such as voice or music.</param>
        /// <returns>Returns 0 on success, or an error code otherwise.</returns>
        [DllImport(dllName)]
        public static extern int OpusEncoderSetSignal(
            IntPtr encoder,
            OpusSignal signal);

        /// <summary>
        /// Creates a new Opus decoder instance.
        /// </summary>
        /// <param name="samplingFrequency">The sampling frequency to be used by the decoder.</param>
        /// <param name="channels">The number of audio channels (mono or stereo).</param>
        /// <param name="error">Outputs an error code indicating success or failure.</param>
        /// <returns>A pointer to the newly created Opus decoder instance.</returns>
        [DllImport(dllName)]
        public static extern IntPtr OpusDecoderCreate(
            SamplingFrequency samplingFrequency,
            NumChannels channels,
            out ErrorCode error);

        /// <summary>
        /// Decodes Opus data into PCM samples.
        /// </summary>
        /// <param name="decoder">A pointer to the Opus decoder instance.</param>
        /// <param name="data">The input buffer containing encoded Opus data.</param>
        /// <param name="len">The length of the input buffer.</param>
        /// <param name="pcm">The output buffer for the decoded PCM samples (16-bit signed integers).</param>
        /// <param name="frameSize">The number of samples per frame.</param>
        /// <param name="decodeFec">Indicates if forward error correction (FEC) is enabled (0 or 1).</param>
        /// <returns>The number of decoded samples, or an error code if failed.</returns>
        [DllImport(dllName)]
        public static extern int OpusDecode(
            IntPtr decoder,
            byte[] data,
            int len,
            short[] pcm,
            int frameSize,
            int decodeFec);

        /// <summary>
        /// Decodes Opus data into PCM samples in floating-point format.
        /// </summary>
        /// <param name="decoder">A pointer to the Opus decoder instance.</param>
        /// <param name="data">The input buffer containing encoded Opus data.</param>
        /// <param name="len">The length of the input buffer.</param>
        /// <param name="pcm">The output buffer for the decoded PCM samples (32-bit floating-point).</param>
        /// <param name="frameSize">The number of samples per frame.</param>
        /// <param name="decodeFec">Indicates if forward error correction (FEC) is enabled (0 or 1).</param>
        /// <returns>The number of decoded samples, or an error code if failed.</returns>
        [DllImport(dllName)]
        public static extern int OpusDecodeFloat(
            IntPtr decoder,
            byte[] data,
            int len,
            float[] pcm,
            int frameSize,
            int decodeFec);

        /// <summary>
        /// Destroys an Opus decoder instance, freeing its allocated resources.
        /// </summary>
        /// <param name="decoder">A pointer to the Opus decoder instance to be destroyed.</param>
        [DllImport(dllName)]
        public static extern void OpusDecoderDestroy(
            IntPtr decoder);

        /// <summary>
        /// Applies soft clipping to PCM samples to prevent distortion.
        /// </summary>
        /// <param name="pcm">The buffer containing PCM samples (32-bit floating-point).</param>
        /// <param name="frameSize">The number of samples per frame.</param>
        /// <param name="channels">The number of audio channels.</param>
        /// <param name="softclipMem">A memory buffer for maintaining the soft clip state.</param>
        [DllImport(dllName)]
        public static extern void OpusPcmSoftClip(
            float[] pcm,
            int frameSize,
            NumChannels channels,
            float[] softclipMem);
    }
}
