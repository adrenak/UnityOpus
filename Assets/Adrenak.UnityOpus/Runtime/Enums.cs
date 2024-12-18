namespace Adrenak.UnityOpus {
    /// <summary>
    /// Defines supported sampling frequencies for the Opus codec.
    /// </summary>
    public enum SamplingFrequency : int {
        /// <summary>
        /// 8 kHz sampling frequency, typically used for narrowband audio.
        /// </summary>
        Frequency_8000 = 8000,

        /// <summary>
        /// 12 kHz sampling frequency, suitable for medium-band audio.
        /// </summary>
        Frequency_12000 = 12000,

        /// <summary>
        /// 16 kHz sampling frequency, commonly used for wideband speech.
        /// </summary>
        Frequency_16000 = 16000,

        /// <summary>
        /// 24 kHz sampling frequency, providing good quality for music and audio.
        /// </summary>
        Frequency_24000 = 24000,

        /// <summary>
        /// 48 kHz sampling frequency, offering the highest audio quality.
        /// </summary>
        Frequency_48000 = 48000,
    }

    /// <summary>
    /// Specifies the number of audio channels.
    /// </summary>
    public enum NumChannels : int {
        /// <summary>
        /// Mono audio (single channel).
        /// </summary>
        Mono = 1,

        /// <summary>
        /// Stereo audio (two channels).
        /// </summary>
        Stereo = 2,
    }

    /// <summary>
    /// Represents the target application for the Opus encoder.
    /// </summary>
    public enum OpusApplication : int {
        /// <summary>
        /// Optimized for VoIP applications (e.g., speech communication).
        /// </summary>
        VoIP = 2048,

        /// <summary>
        /// Optimized for general audio applications (e.g., music streaming).
        /// </summary>
        Audio = 2049,

        /// <summary>
        /// Optimized for low-delay applications (e.g., real-time audio).
        /// </summary>
        RestrictedLowDelay = 2051,
    }

    /// <summary>
    /// Specifies the signal type for the Opus encoder.
    /// </summary>
    public enum OpusSignal : int {
        /// <summary>
        /// Automatically detects the signal type (default behavior).
        /// </summary>
        Auto = -1000,

        /// <summary>
        /// Optimized for voice signals (e.g., speech).
        /// </summary>
        Voice = 3001,

        /// <summary>
        /// Optimized for music signals.
        /// </summary>
        Music = 3002
    }

    /// <summary>
    /// Represents error codes that may be returned by Opus functions.
    /// </summary>
    public enum ErrorCode {
        /// <summary>
        /// No error occurred; the operation was successful.
        /// </summary>
        OK = 0,

        /// <summary>
        /// One or more invalid arguments were provided.
        /// </summary>
        BadArg = -1,

        /// <summary>
        /// The provided buffer was too small to hold the output data.
        /// </summary>
        BufferTooSmall = -2,

        /// <summary>
        /// An internal error occurred within the Opus library.
        /// </summary>
        InternalError = -3,

        /// <summary>
        /// The input packet is invalid or corrupted.
        /// </summary>
        InvalidPacket = -4,

        /// <summary>
        /// The requested operation has not been implemented.
        /// </summary>
        Unimplemented = -5,

        /// <summary>
        /// The encoder or decoder is in an invalid state.
        /// </summary>
        InvalidState = -6,

        /// <summary>
        /// Memory allocation failed during the operation.
        /// </summary>
        AllocFail = -7,
    }
}