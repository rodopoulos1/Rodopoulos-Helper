# CS2 Help Menu Plugin

## About

This is a basic plugin that adds a customizable help menu with personalized commands, primarily designed to assist with server commands on a CS2 server. All settings are fully configurable.

## Dependencies

- None

## Language Files

The plugin supports two languages: English (`en`) and Portuguese (`pt-br`).

**Example Language JSON:**

```json
{
    "Help Menu Title": "Help Menu",
    "Help Menu Item": "{0}",
    "Help Menu Options title": "Description",
    "Help Menu Options item": "{0}",
    "Help Menu Back": "Back"
}
```
## Configuration
```json
{
  "HelpCommands": [
    "help",
    "ajuda"
  ],
  "CommandItem": [
    {
      "Command": "!help",
      "Options": [
        {
          "Description": "Give help to a player"
        }
      ]
    },
    {
      "Command": "!rtv",
      "Options": [
        {
          "Description": "Rock the vote"
        }
      ]
    }                       
  ],
  "ConfigVersion": 1
}
```
