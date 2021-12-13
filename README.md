# placeholder-image-api
This api takes any size image and will create a placeholder on a project when certain assets are missing

# Getting started -
## V1 - 
The user can input any size image they want and be returned an image that is that size from the endpoint of /noaspectratio/{width}/{height}

# TODO
```
Add cacheing to store the first n images in a dictionary and allow users to pull certain aspect ratios of pictures into projects
```

# SOURCES

# #Nature -
Nature source will return pictures of nature to the user.

# Kitten - 
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
