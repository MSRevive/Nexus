# Changelog

## v1.0.0-rc2
### Changes
* SteamID64 now use ulong instead of string.

## v1.0.0-rc1
### Added
* Added GET method to retrieve if steamid64 is banned.
### Changes
* Combined all the bags to a single "Bags" field.
### Fixes
* Fixed map check returning true when it should be false.
* Fixed unhandled exception if map or ban list files don't exists.

## v0.0.2-rc2
### Changes
* Renamed Shortcuts to Quickslots to be more concise.
* Only create fake database records under DEBUG environment.

## v0.0.1-rc1
Initial release.
