## UnityOpus 🔉
Note: A UPM fork of [UnityOps by TyounanMOTI](https://github.com/TyounanMOTI/UnityOpus) with some XML comments. The original project is sponsored by Cluster, Inc. 

[Scripting reference](http://www.vatsalambastha.com/UnityOpus)

## Installation 📦
Ensure you have the NPM registry in the `packages.json` file of your Unity project with `com.adrenak.unityops` as one of the scopes
```
"scopedRegistries": [
    {
        "name": "npmjs",
        "url": "https://registry.npmjs.org",
        "scopes": [
            "com.npmjs",
            "com.adrenak.unityopus"
        ]
    }
}
```

Add `"com.adrenak.unityopus" : "x.y.z"` to `dependencies` list in `packages.json` file where `x.y.z` is the version name

## Tip
The [Encoder.Encode](https://www.vatsalambastha.com/UnityOpus/api/Adrenak.UnityOpus.Encoder.html#Adrenak_UnityOpus_Encoder_Encode_System_Single___System_Byte___0) returns an int.  
If it's > 0, encoding succeeded and this int represents the length of the encoded audio in the output array. You'll need to copy the encoded audio from the output.  
If it's < 0, encoding has failed and the int actually represents the error code

```
// Assuming pcm is the float[] you want to encode
// The output array should be equal to the pcm array size in bytes, hence multuplied by 4
byte[] output = new byte[pcm.Length * 4];
int encodeResult = encoder.Encode(pcm, output); 
if(encodedResult > 0) {
    byte[] encodedBytes = new byte[encodeResult];
    Array.Copy(encodeBuffer, encodedBytes, encodedBytes.Length);
    // encodedBytes now has the encoded audio ready for your use
}
else {
    // Get the error
    ErrorCode errorCode = (ErrorCode)encodeResult;
}

```

## Contact 👥
[@github](https://www.github.com/adrenak)  
[@website](http://www.vatsalambastha.com)  
@discord: `adrenak#1934`