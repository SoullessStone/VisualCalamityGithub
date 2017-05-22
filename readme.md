# Setup
VisualCalamity, a Project made during the Mobiliar ChallengeDays Hackathon in Bern (CH). 
To run it, you will need:
- [Unity 5.6.1f](https://unity3d.com/get-unity/download?thank-you=update&download_nid=47062&os=Win) 
- [Holotoolkit-Unity v 1.5.7.0](https://github.com/Microsoft/HoloToolkit-Unity/releases)

# How to use it?
This program enable you to tag important object in your world using the vocal command *set Value*, a square will appear and will ahve a value of CHF 1000 (TODO change value)
With the command *Take Snap*, a "screenshot" from the current user vision is going to be taken and saved on an Azure Blob Storage.
With the command *Load Snap* and staring at a white rectangle, the screenshot associated with the current value item will be loaded from Azure.
The command *fire start* start the firemode, enabling a minigame where you can burn down items from the scene. Idea would be in future version to display insurance coverage