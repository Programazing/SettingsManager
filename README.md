# Settings Manager

This is a basic settings manager I made using Json to keep track of a few user settings.

`Settings` is strongly typed using the `SettingsModel`. Add whatever settings you want to the model.

Be sure to change "YourProjectName" in `AppFolderPath = $"{appData}/YourProjectName";` to whatever the name of your published project will be that way it shows up correctly under `C:\Users\UserName\AppData\Roaming`

The project will create a new folder and settings file if it doesn't find your folder in `Roaming` when `SettingsManager` is called. It will also populate the `Settings` member at  the same time.

To use the project just make a reference to it in the calling project. You might want to change the namespace in `SettingsManager` to whatever your namespace is or you'll have to call it as `SettingsManager.SettingsManager.Settings`

```csharp
// Get Settings
var settings = SettingsManager.Settings;

//Change a Setting
settings.SomeSetting = "New Info";

//Update Settings
SettingsManager.SetSettings(settings);

//Get a new copy of Settings
var output = SettingsManager.Settings;

//output will be "New Info" instead of the default "data"
Console.WriteLine(output.SomeSetting);
```
