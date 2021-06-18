# [C++ Mate] a Visual Studio extension
![alt text](https://github.com/rtojagic/cppmate/blob/main/img/cppmate_logo.png "C++ Mate")

C++ Mate is a Visual Studio extension that makes working with C++ projects an enjoyable experience.

Supported versions:
- VS 2019

## Word of the author
As a C++ game developer with a passion of always learning new things about games, game engines, new language features I found a need for a tool that would allow me to organize my projects in a more sophisticated way. By default, Visual Studio would not support such an organization very well and would force me to use filters instead of folders, or use folder organization while keeping the project / source files in the same directory which is bad for sharing the project across different platforms and also makes life more difficult when using a tool like CMake, Sharpmake, Premake... Thus the idea was born, to create this plugin that allows you to use filters as folders, create new folders, create new source files at the desired location (now you can actually split source files from VS project files!) with additional control and options. Hope you enjoy it! :)

## Features
### Before you begin
When you run a new project for the first time, with the C++ Mate enabled it will greet you with a welcoming screen. Here you will be asked to select the root directory of your source files, wherever it may be. If you generated the projects using Sharpmake, Premake, Cmake or anything similar, Visual Studio should already generate filters based on the source directory structure, and C++ Mate will help you maintain such a structure.

![alt text](https://github.com/rtojagic/cppmate/blob/main/img/cppmate_0.jpg "C++ Mate")

### Create new folder
Right clicking on a filter in the project view, a menu will popup. There you will find C++ Mate commands to create a folder or a source file.

Creating a new folder will also add the filter of the same name to the project. 

![alt text](https://github.com/rtojagic/cppmate/blob/main/img/cppmate_1.jpg "C++ Mate")

### Create new file
When creating a new file you will be presented with variety of options to chose from.

![alt text](https://github.com/rtojagic/cppmate/blob/main/img/cppmate_2.jpg "C++ Mate")

