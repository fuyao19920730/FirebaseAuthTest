//
//  GoogleSignPlugins.h
//  Unity-iPhone
//
//  Created by 傅瑶fotoable on 2019/9/4.
//



/*********** Method Names ***********/
const char * _Nonnull UNITY_DEVICE_CALLBACK = "DeviceInfoCallback";

/*********** google sign in Unity 消息接受对象 ***********/
const char * _Nonnull GOOGLE_OBJECT_NAME = "UnityGoogleSignInPlugin";




#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface GoogleSignPlugin : NSObject
+ (instancetype)shared;
- (void)TestCridential;
- (void)Configure;

@end

NS_ASSUME_NONNULL_END

