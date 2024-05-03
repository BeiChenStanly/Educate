# Changelog

All notable changes to this package will be documented in this file.

## [1.0.10] - 2024-04-23
*Add content type by file extension

## [1.0.9] - 2024-03-31
*Auto upload Code folder for .net 8 backend

## [1.0.8] - 2024-03-14
*Fix a issue on calculating streaming scene count

## [1.0.7] - 2024-03-07
* Fix UOS badge tag moved to old release if badges are changed outside the autostreaming UI.
* Clear scene streaming config file if scene streaming is cancled
* For kwai game, use il2cpp backend if it is tuanjie engine

## [1.0.6] - 2024-01-08
* Fix UOS CDN download host

## [1.0.5] - 2023-12-28
* Rename UOS Secret to UOS Service Secret
* Fix UOS CDN upload problem when using a VPN

## [1.0.4] - 2023-11-02
* Fix missing addressable editor reference
* Android Native InstantGame: add platform "alipay"
* AutoStreaming UI: add warning hints to Mesh assets with read/write enabled

## [1.0.3] - 2023-09-15
* Validate test assembly definition file

## [1.0.2] - 2023-09-08
* Add Tests to package

## [1.0.1] - 2023-09-01

## [1.0.0] - 2023-08-31
* Support Tuanjie Engine 2022.3.2t1

## [0.3.2] - 2023-07-21
* Support 2021.2.5f1c303

## [0.3.1] - 2023-06-07
* Add support for wechat minigame SDK

## [0.3.0] - 2023-01-04
* Improve placeholder generation[placeholder 2]

## [0.2.2] - 2023-02-24
* Error dialog shows detail if UOS CDN not enbaled

## [0.2.1] - 2022-12-22
* Improve upload speed

## [0.2.0] - 2022-12-16
* Migrate from Unity CCD to Unity Online Service CDN

## [0.1.9] - 2022-08-24
* Add a fix for assetbundle tool

## [0.1.8] - 2022-08-22
* Add predownload functions for 2019.4.29f1c110
* Add asset bundle dependencies analysis tool
* Support setting default material for model importing
* Add clear cache function
* Fix assertion fails on machines after Android 10 with mono 64bits

## [0.1.7] - 2022-01-11
* Fix searh out assets from addressable.
* Add a search field and wanings for fbx in shared assets panel.
* Add a hotfix for Android5 path length limitation for bytedance.
* Add "Search AB" button for generating "CustomSearch.txt" from AB manifest.

## [0.1.6] - 2021-12-21
* package now depends on nuget.mono-cecil@0.1.6-preview.

## [0.1.5] - 2021-12-16
* Add Shared Assets UI in scene streaming tab.
* Add Build Instant Game warnings.
* Add search bar for all synced assets.
* Add Animation Streaming UI.
* Rename "Configuration" tab to "Publish" tab.

## [0.1.4] - 2021-09-17
* Merge packages `Auto Streaming` and `Auto Streaming CCD` into package `Instant Game`.
