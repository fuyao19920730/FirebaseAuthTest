//
//  GoogleSignPlugins.m
//  Unity-iPhone
//
//  Created by 傅瑶fotoable on 2019/9/4.
//

#import "GoogleSignPlugin.h"
#import <GoogleSignIn/GoogleSignIn.h>
#import <Firebase.h>


#if defined(__cplusplus)
extern "C"{
#endif
    //供u3d调用的c函数
    void GoogleSignIn(){
        [[GoogleSignPlugin shared] TestCridential];
    }
    
    void GoogleInit(){
        [[GoogleSignPlugin shared] Configure];
    }
    
#if defined(__cplusplus)
}
#endif



@interface GoogleSignPlugin ()
<
UIApplicationDelegate,
GIDSignInDelegate
>

@end
@implementation GoogleSignPlugin


+ (instancetype)shared {
    static GoogleSignPlugin *instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[self alloc] init];
    });
    return instance;
}

- (void)Configure
{
    // Use Firebase library to configure APIs
    [GIDSignIn sharedInstance].clientID = @"1070696458019-r29pla1gf86ih4trdv10lbvouetgl5rt.apps.googleusercontent.com";
    [GIDSignIn sharedInstance].delegate = self;
}


- (void)signIn:(GIDSignIn *)signIn
didSignInForUser:(GIDGoogleUser *)user
     withError:(NSError *)error {
    // ...
    if (error == nil) {
        //登录成功
        GIDAuthentication *authentication = user.authentication;
        //换取firebase 凭证
        FIRAuthCredential *credential = [FIRGoogleAuthProvider credentialWithIDToken:authentication.idToken
                                         accessToken:authentication.accessToken];
        //fire base 验证拿到用户信息
        [[FIRAuth auth] signInWithCredential:credential
                                  completion:^(FIRAuthDataResult * _Nullable authResult,
                                               NSError * _Nullable error) {
                                      if (error) {
                                          // ...
                                          return;
                                      }
                                      // User successfully signed in. Get user data from the FIRUser object
                                      if (authResult == nil) { return; }
                                      FIRUser *user = authResult.user;
                                      // ...
                                  }];
        
    } else {
        // ...
    }
}
- (void)signIn:(GIDSignIn *)signIn
didDisconnectWithUser:(GIDGoogleUser *)user
     withError:(NSError *)error {
    // Perform any operations when the user disconnects from app here.
    // ...
}

- (void)TestCridential{
    //
    [FIRGameCenterAuthProvider getCredentialWithCompletion:^(FIRAuthCredential * _Nullable credential, NSError * _Nullable error) {
        
    }];
}
@end
