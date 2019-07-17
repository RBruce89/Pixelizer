# Pixelizer

![Giphy](https://media.giphy.com/media/XEaJM9IKlWyFhon4gH/giphy.gif)

# Introduction

Pixelizer was created as a fun way to encrypt plain text. First it converts every character to its binary value. Then it scrambles them using simple entropy and a pseudo random generator, and hides the key somewhere in with the other values. Finally it uses the scrambled character values as red, blue, green, and alpha settings for individual pixels in an image it creates.
Pixelizer can also read images it created and convert them back to plain text.

## Features

#### Quickly convert plain text to image of random looking pixels

#### Quickly convert created pixel image back to plain text

#### Program supports
* Opening .txt and .png files by browsing the computer’s directory
* Drag and dropping .txt and .png files
* Or just typing it in plain text

#### Files can also be saved by browsing the computer’s directory.

## Technical information

Pixelizer was written in C#.NET version 4.6.1
using Visual Studio version 15.027428.1
