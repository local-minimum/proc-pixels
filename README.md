# proc-pixels
Procedural pixel-sprite generation plugin for unity

*This is currently in a very early development phase*

The plugin consists of three parts:
 * Colors and their Palettes used to (randomly) seed what to paint with
 * The canvas object and its helpers which creates and keep track of updates to the Texture2D as well as has the ability to
 save out its content.
 * Artists that use colors to convert vector representations of what they are drawing into pixels.
 They don't have to use vectors, but those I've implemented do.
 
# Demo implementations

The project contains a prefabs with palettes for human skin, eye, hair color.
It also contains example of how these can be put together as a face-meta palette.

## Face Artist

The face artist is a facial painter. It consists of several subpainters that ineherit information from their parent.
E.g. the eyes position themselves based on the polygon outline of the face, then the eyebrows positions themselves based on the
outline of the eye.
 
