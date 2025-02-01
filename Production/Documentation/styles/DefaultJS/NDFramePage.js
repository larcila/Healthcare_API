﻿/*
This file is part of Natural Docs, which is Copyright © 2003-2023 Code Clear LLC.
Natural Docs is licensed under version 3 of the GNU Affero General Public
License (AGPL).  Refer to License.txt or www.naturaldocs.org for the
complete details.

This file may be distributed with documentation files generated by Natural Docs.
Such documentation is not covered by Natural Docs' copyright and licensing,
and may have its own copyright and distribution terms as decided by its author.
*/

"use strict";var NDFramePage=new function(){this.Start=function(){this.resizeEventHandler=NDFramePage.OnResize.bind(NDFramePage);this.blurEventHandler=NDFramePage.OnBlur.bind(NDFramePage);this.hashChangeEventHandler=NDFramePage.OnHashChange.bind(NDFramePage);this.mouseDownEventHandler=NDFramePage.OnMouseDown.bind(NDFramePage);this.sizerMouseMoveEventHandler=NDFramePage.OnSizerMouseMove.bind(NDFramePage);this.sizerMouseUpEventHandler=NDFramePage.OnSizerMouseUp.bind(NDFramePage);this.effectiveThemeChangeEventHandler=NDFramePage.OnEffectiveThemeChange.bind(NDFramePage);this.availableThemesChangeEventHandler=NDFramePage.OnAvailableThemesChange.bind(NDFramePage);this.projectTitle=document.title;var loadingNotice=document.getElementById("NDLoadingNotice");loadingNotice.parentNode.removeChild(loadingNotice);var pageElements={NDHeader:true,NDThemeSwitcher:NDThemeSwitcher.IsNeeded(),NDSearchField:true,NDFooter:true,NDMenu:true,NDSummary:false,NDContent:true,NDMenuSizer:true,NDSummarySizer:true};for(var pageElementName in pageElements){var domElement=document.getElementById(pageElementName);domElement.style.position="fixed";if(pageElements[pageElementName]==true){domElement.style.display="block";}}this.UpdateLayout();window.addEventListener("resize",this.resizeEventHandler);document.addEventListener("mousedown",this.mouseDownEventHandler);document.addEventListener("NDEffectiveThemeChange",this.effectiveThemeChangeEventHandler);document.addEventListener("NDAvailableThemesChange",this.availableThemesChangeEventHandler);window.addEventListener("blur",this.blurEventHandler);NDMenu.Start();NDSummary.Start();NDSearch.Start();NDThemeSwitcher.Start();};this.OnBlur=function(event){if(NDSearch.SearchFieldIsActive()){NDSearch.ClearResults();NDSearch.DeactivateSearchField();}if(NDThemeSwitcher.MenuIsOpen()){NDThemeSwitcher.CloseMenu();}};this.OnHashChange=function(event){var oldLocation=this.currentLocation;this.currentLocation=new NDLocation(location.hash);NDSearch.ClearResults();NDSearch.DeactivateSearchField();if(this.currentLocation.type=="Home"&&this.sourceFileHomePageHashPath){var homePageLocation=new NDLocation(this.sourceFileHomePageHashPath);homePageLocation.type="Home";this.currentLocation=homePageLocation;}var oldLocationHasSummary=(oldLocation!=undefined&&oldLocation.summaryFile!=undefined);var currentLocationHasSummary=(this.currentLocation.summaryFile!=undefined);if(oldLocationHasSummary!=currentLocationHasSummary){this.UpdateLayout();}var frame=document.getElementById("CFrame");var newLocation=this.currentLocation.contentPage;if(NDThemes.effectiveThemeID!=undefined){newLocation=NDCore.AddQueryParams(newLocation,"Theme="+NDThemes.effectiveThemeID);}frame.contentWindow.location.replace(newLocation);frame.contentWindow.focus();NDMenu.OnLocationChange(oldLocation,this.currentLocation);NDSummary.OnLocationChange(oldLocation,this.currentLocation);if(this.currentLocation.summaryFile==undefined){this.UpdatePageTitle();}};this.OnLocationsLoaded=function(locationInfo,sourceFileHomePageHashPath){this.locationInfo=locationInfo;for(var i=0;i<this.locationInfo.length;i++){this.locationInfo[i][4]=new RegExp(this.locationInfo[i][3]);}this.sourceFileHomePageHashPath=sourceFileHomePageHashPath;window.addEventListener("hashchange",this.hashChangeEventHandler);this.OnHashChange();};this.OnPageTitleLoaded=function(hashPath,title){if(this.currentLocation.path==hashPath){if(this.currentLocation.type=="Home"){this.UpdatePageTitle();}else{this.UpdatePageTitle(title);}}};this.UpdatePageTitle=function(pageTitle){if(pageTitle){document.title=pageTitle+" - "+this.projectTitle;}else{document.title=this.projectTitle;}};this.OnResize=function(event){this.UpdateLayout();};this.UpdateLayout=function(){var fullWidth=window.innerWidth;var fullHeight=window.innerHeight;var header=document.getElementById("NDHeader");var themeSwitcher=document.getElementById("NDThemeSwitcher");var searchField=document.getElementById("NDSearchField");var footer=document.getElementById("NDFooter");var menu=document.getElementById("NDMenu");var menuSizer=document.getElementById("NDMenuSizer");var summary=document.getElementById("NDSummary");var summarySizer=document.getElementById("NDSummarySizer");var content=document.getElementById("NDContent");header.style.left="0px";header.style.top="0px";header.style.width=fullWidth+"px";var headerHeight=header.offsetHeight-1;var headerTitle=document.getElementById("HTitle").firstChild;var headerTitleRightEdge=headerTitle.offsetLeft+headerTitle.offsetWidth;var headerSubTitle=document.getElementById("HSubtitle");var headerSubTitleRightEdge=0;if(headerSubTitle){headerSubTitle=headerSubTitle.firstChild;headerSubTitleRightEdge=headerSubTitle.offsetLeft+headerSubTitle.offsetWidth;}var headerRightEdge=Math.max(headerTitleRightEdge,headerSubTitleRightEdge);if(this.desiredSearchWidth==undefined){this.desiredSearchWidth=searchField.offsetWidth;}var searchMargin=Math.floor((headerHeight-searchField.offsetHeight)/2);var searchWidth=this.desiredSearchWidth;var maxSearchWidth=fullWidth-headerRightEdge-(searchMargin*4);var minSearchWidth=searchMargin*5;var themeSwitcherSize;if(NDThemeSwitcher.IsNeeded()){themeSwitcherSize=searchField.offsetHeight;maxSearchWidth-=themeSwitcherSize+searchMargin;}if(searchWidth>maxSearchWidth){searchWidth=maxSearchWidth;}if(searchWidth<minSearchWidth){searchWidth=minSearchWidth;}searchField.style.left=(fullWidth-searchWidth-searchMargin)+"px";searchField.style.top=searchMargin+"px";searchField.style.width=searchWidth+"px";if(NDThemeSwitcher.IsNeeded()){themeSwitcher.style.left=(fullWidth-searchField.offsetWidth-searchMargin-themeSwitcherSize-Math.floor(searchMargin/2))+"px";themeSwitcher.style.top=searchMargin+"px";themeSwitcher.style.width=themeSwitcherSize+"px";themeSwitcher.style.height=themeSwitcherSize+"px";}var remainingHeight=fullHeight-headerHeight;var remainingWidth=fullWidth;var currentX=0;var currentY=headerHeight;menu.style.display="block";menu.style.left=currentX+"px";menu.style.top=currentY+"px";var menuWidth=menu.offsetWidth;menu.style.width=menuWidth+"px";footer.style.left=currentX+"px";footer.style.top=currentY+"px";footer.style.width=menuWidth+"px";var footerHeight=footer.offsetHeight;menu.style.height=(remainingHeight-footerHeight)+"px";footer.style.top=(currentY+remainingHeight-footerHeight)+"px";if(this.desiredMenuWidth==undefined){this.desiredMenuWidth=menuWidth;}menuSizer.style.display="block";menuSizer.style.left=currentX+menuWidth+"px";menuSizer.style.top=currentY+"px";menuSizer.style.height=remainingHeight+"px";NDMenu.OnUpdateLayout();currentX+=menuWidth;remainingWidth-=menuWidth;if(this.SummaryIsVisible()){summary.style.display="block";summary.style.left=currentX+"px";summary.style.top=currentY+"px";summary.style.height=remainingHeight+"px";var summaryWidth=summary.offsetWidth;summary.style.width=summaryWidth+"px";if(this.desiredSummaryWidth==undefined){this.desiredSummaryWidth=summaryWidth;}summarySizer.style.display="block";summarySizer.style.left=(currentX+summaryWidth-1)+"px";summarySizer.style.top=currentY+"px";summarySizer.style.height=remainingHeight+"px";currentX+=summaryWidth;remainingWidth-=summaryWidth;}else{summary.style.display="none";summarySizer.style.display="none";}content.style.left=currentX+"px";content.style.top=currentY+"px";content.style.width=remainingWidth+"px";content.style.height=remainingHeight+"px";NDSearch.OnUpdateLayout();NDThemeSwitcher.OnUpdateLayout();};this.SummaryIsVisible=function(){return(this.currentLocation!=undefined&&this.currentLocation.summaryFile!=undefined);};this.OnMouseDown=function(event){var target=event.target;if(NDSearch.SearchFieldIsActive()){var targetIsPartOfSearch=false;for(var element=target;element!=undefined;element=element.parentNode){if(element.id=="NDSearchResults"||element.id=="NDSearchField"){targetIsPartOfSearch=true;break;}}if(!targetIsPartOfSearch){NDSearch.ClearResults();NDSearch.DeactivateSearchField();}}if(NDThemeSwitcher.MenuIsOpen()){var targetIsPartOfThemeSwitcher=false;for(var element=target;element!=undefined;element=element.parentNode){if(element.id=="NDThemeSwitcher"||element.id=="NDThemeSwitcherMenu"){targetIsPartOfThemeSwitcher=true;break;}}if(!targetIsPartOfThemeSwitcher){NDThemeSwitcher.CloseMenu();}}if(target.id=="NDMenuSizer"||target.id=="NDSummarySizer"){var panel;if(target.id=="NDMenuSizer"){panel=document.getElementById("NDMenu");}else{panel=document.getElementById("NDSummary");}this.sizerDragging={"sizer":target,"panel":panel,"originalSizerX":target.offsetLeft,"originalPanelWidth":panel.offsetWidth,"originalClientX":event.clientX};target.classList.add("Dragging");document.addEventListener("mousemove",this.sizerMouseMoveEventHandler);document.addEventListener("mouseup",this.sizerMouseUpEventHandler);var contentCover=document.createElement("div");contentCover.id="NDContentCover";contentCover.style.left="0px";contentCover.style.top="0px";contentCover.style.width=window.innerWidth+"px";contentCover.style.height=window.innerHeight+"px";document.body.appendChild(contentCover);event.preventDefault();}else if(target.tagName=="A"&&target.href==window.location){this.OnHashChange(event);event.preventDefault();}};this.OnSizerMouseMove=function(event){var offset=event.clientX-this.sizerDragging.originalClientX;var windowClientWidth=window.innerWidth;if(this.sizerDragging.sizer.id=="NDMenuSizer"){if(this.sizerDragging.originalSizerX+offset<0){offset=0-this.sizerDragging.originalSizerX;}else if(this.sizerDragging.originalSizerX+offset+this.sizerDragging.sizer.offsetWidth>windowClientWidth){offset=windowClientWidth-this.sizerDragging.sizer.offsetWidth-this.sizerDragging.originalSizerX;}}else{var menuSizer=document.getElementById("NDMenuSizer");var leftLimit=menuSizer.offsetLeft+menuSizer.offsetWidth;if(this.sizerDragging.originalSizerX+offset<leftLimit){offset=leftLimit-this.sizerDragging.originalSizerX;}else if(this.sizerDragging.originalSizerX+offset+this.sizerDragging.sizer.offsetWidth>windowClientWidth){offset=windowClientWidth-this.sizerDragging.sizer.offsetWidth-this.sizerDragging.originalSizerX;}}this.sizerDragging.sizer.style.left=(this.sizerDragging.originalSizerX+offset)+"px";this.sizerDragging.panel.style.width=(this.sizerDragging.originalPanelWidth+offset)+"px";if(this.sizerDragging.sizer.id=="NDMenuSizer"){this.desiredMenuWidth=document.getElementById("NDMenu").offsetWidth;}else{this.desiredSummaryWidth=document.getElementById("NDSummary").offsetWidth;}this.UpdateLayout();};this.OnSizerMouseUp=function(event){document.removeEventListener("mousemove",this.sizerMouseMoveEventHandler);document.removeEventListener("mouseup",this.sizerMouseUpEventHandler);document.body.removeChild(document.getElementById("NDContentCover"));this.sizerDragging.sizer.classList.remove("Dragging");this.sizerDragging=undefined;};this.SizeSummaryToContent=function(){this.SizePanelToContent(document.getElementById("NDSummary"),this.desiredSummaryWidth);};this.SizePanelToContent=function(panel,desiredOffsetWidth){if(this.desiredSummaryWidth==undefined){return;}var resized=false;if(panel.clientWidth==panel.scrollWidth){if(panel.offsetWidth==desiredOffsetWidth){return;}else{panel.style.width=desiredOffsetWidth+"px";resized=true;}}var newOffsetWidth=panel.scrollWidth;if(panel.scrollHeight>panel.clientHeight){newOffsetWidth+=panel.offsetWidth-panel.clientWidth;}else{newOffsetWidth+=NDCore.GetComputedPixelWidth(panel,"borderLeftWidth")+NDCore.GetComputedPixelWidth(panel,"borderRightWidth");}if(newOffsetWidth!=desiredOffsetWidth){newOffsetWidth+=3;if(newOffsetWidth/desiredOffsetWidth>1.333){newOffsetWidth=Math.floor(desiredOffsetWidth*1.333);}if(panel.offsetWidth!=newOffsetWidth){panel.style.width=newOffsetWidth+"px";resized=true;}}if(resized){this.UpdateLayout();}};this.OnEffectiveThemeChange=function(event){if(event.detail.oldEffectiveThemeID!=undefined){document.documentElement.classList.remove(event.detail.oldEffectiveThemeID+"Theme");}if(event.detail.newEffectiveThemeID!=undefined){document.documentElement.classList.add(event.detail.newEffectiveThemeID+"Theme");}var frame=document.getElementById("CFrame");if(NDThemes.effectiveThemeID!=undefined){frame.contentWindow.postMessage("Theme="+NDThemes.effectiveThemeID,"*");}else{frame.contentWindow.postMessage("NoTheme","*");}};this.OnAvailableThemesChange=function(event){if(NDThemeSwitcher.UpdateVisibility()){this.UpdateLayout();}};};