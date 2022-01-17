# placeholder-image-api
This api takes any size image and will create a placeholder on a project when certain assets are missing

# Getting started -
## V2 -
Added Azure deployment 
```bash
https://image-placeholder-api.azurewebsites.net/nature/noaspectratio
```
- Added a new endpoint that has option parameters of width, height, initialHeight, and initialWidth.
- Added IOptions pattern to cache the top n most common image sizes
## V1 - 
The user can input any size image they want and be returned an image that is that size from the endpoint of /noaspectratio/{width}/{height}

# TODO
- Finish algorithm that gets image for client closest to their desired aspect ratio

# SOURCES

## Nature -
Nature source will return pictures of nature to the user.

## Kitten - 
Kitten source will return pictures of kitten(s) to the user.

# Endpoints -
## Gathering random images of specific height and wiidth
```
/source/noaspectratio?initialWidth=xyx&initialHeight=xyz
```
## Gathering random images and resizing them
```
/source/noaspectratio?initialWidth=xyx&initialHeight=xyz&width=xyz&height=xyz
```
