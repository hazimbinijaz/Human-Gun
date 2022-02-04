# JustTrack SDK - Injected Code

This folder contains code managed by the JustTrack SDK.

## Why is this needed?

To make it easy to integrate and update the JustTrack SDK it is provieded as a Unity package.
However, this means that the C# code of the JustTrack SDK can't access code from the default
assembly (Assembly-CSharp.dll). This is normally a good thing and can speed up builds by some
bit, but it also comes with some drawbacks. Mainly, some SDKs like the IronSource SDK are
provided as a .unitypackage by default and therefore end up in the default assembly if no futher
changes are made to them by a developer. This means, we can only access them via reflection at
runtime.

For the most part, using reflection is sufficient to perform the needed tasks. However, sometimes
we need to generate a small amount of code at runtime and this might not be supported if you are
using the IL2CPP backend instead of the Mono backend for greater speed. Thus, we need to add some
small bridging code to the default assembly to avoid having to generate that code at runtime.

## Can I edit these files?

There should be no need to change any of these files and your changes might be overwritten at any
time during futher development by the code which generated them in the first place. Instead, if
you need to make some changes, please tell us why you need to change them, what needs to be
changed and we will find a solution fitting you and improving the JustTrack SDK for everyone
else, too. You can contact us at <mailto:contact@justtrack.io> or <https://justtrack.io/contact/>.

## Should I add these files to version control?

You should add the files in this folder to version control. This will make it simpler along your
team to work with the JustTrack SDK as not every developer has to make the decision whether to
actually create the generated code in the first place. It is also needed if you are building your
game in some CI pipeline to ensure a consistent version of your game is produced every time.
