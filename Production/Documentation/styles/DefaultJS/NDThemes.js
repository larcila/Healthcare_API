﻿/*
This file is part of Natural Docs, which is Copyright © 2003-2023 Code Clear LLC.
Natural Docs is licensed under version 3 of the GNU Affero General Public
License (AGPL).  Refer to License.txt or www.naturaldocs.org for the
complete details.

This file may be distributed with documentation files generated by Natural Docs.
Such documentation is not covered by Natural Docs' copyright and licensing,
and may have its own copyright and distribution terms as decided by its author.
*/

"use strict";var NDThemes=new function(){this.SetCurrentTheme=function(themeID,userSelected){var oldUserSelectedThemeID=this.userSelectedThemeID;var oldEffectiveThemeID=this.effectiveThemeID;var newUserSelectedThemeID=themeID;var newEffectiveThemeID=themeID;if(newUserSelectedThemeID!=undefined&&newUserSelectedThemeID.startsWith("Auto:")){var slashIndex=newUserSelectedThemeID.indexOf("/",5);if(slashIndex!=-1){var systemTheme=this.GetSystemTheme();if(systemTheme==0){newEffectiveThemeID=newUserSelectedThemeID.substring(5,slashIndex);}else if(systemTheme=1){newEffectiveThemeID=newUserSelectedThemeID.substring(slashIndex+1);}}if(this.systemThemeChangeEventHandler==undefined){this.systemThemeChangeEventHandler=this.OnSystemThemeChange.bind(NDThemes);window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change",this.systemThemeChangeEventHandler);}}this.userSelectedThemeID=newUserSelectedThemeID;this.effectiveThemeID=newEffectiveThemeID;if(userSelected){if(this.userSelectedThemeHistory==undefined){this.LoadUserSelectedThemeHistory();}this.AddToUserSelectedThemeHistory(newUserSelectedThemeID);this.SaveUserSelectedThemeHistory();}if(newEffectiveThemeID!=oldEffectiveThemeID){document.dispatchEvent(new CustomEvent("NDEffectiveThemeChange",{detail:{oldUserSelectedThemeID:oldUserSelectedThemeID,oldEffectiveThemeID:oldEffectiveThemeID,newUserSelectedThemeID:newUserSelectedThemeID,newEffectiveThemeID:newEffectiveThemeID,userSelected:userSelected}}));}};this.SetAvailableThemes=function(themes){this.availableThemes=themes;document.dispatchEvent(new Event("NDAvailableThemesChange"));if(this.availableThemes==undefined){this.SetCurrentThemeID(undefined,false);}else{var currentThemeIsInAvailableThemes=false;if(this.userSelectedThemeID!=undefined){for(var i=0;i<this.availableThemes.length;i++){if(this.userSelectedThemeID==this.availableThemes[i][1]){currentThemeIsInAvailableThemes=true;break;}}}if(!currentThemeIsInAvailableThemes){if(this.userSelectedThemeHistory==undefined){this.LoadUserSelectedThemeHistory();}var newThemeID;if(this.userSelectedThemeHistory!=undefined){for(var ui=0;ui<this.userSelectedThemeHistory.length;ui++){for(var ai=0;ai<this.availableThemes.length;ai++){if(this.availableThemes[ai][1]==this.userSelectedThemeHistory[ui]){newThemeID=this.userSelectedThemeHistory[ui];break;}}if(newThemeID!=undefined){break;}}}if(newThemeID==undefined){newThemeID=this.availableThemes[0][1];}this.SetCurrentTheme(newThemeID,false);}}};this.ForceTheme=function(themeID){this.SetCurrentTheme(themeID,false);if(themeID==undefined){this.SetAvailableThemes(undefined);}else{this.SetAvailableThemes([[themeID,themeID]]);}};this.DisableThemes=function(){this.ForceTheme(undefined);};this.LoadUserSelectedThemeHistory=function(){var joinedString=window.localStorage.getItem("NDThemes.UserSelectedThemeHistory");if(joinedString==null){this.userSelectedThemeHistory=undefined;}else{this.userSelectedThemeHistory=joinedString.split(",");}};this.SaveUserSelectedThemeHistory=function(){if(this.userSelectedThemeHistory==undefined){window.localStorage.removeItem("NDThemes.UserSelectedThemeHistory");}else{var joinedString=this.userSelectedThemeHistory.join(",");window.localStorage.setItem("NDThemes.UserSelectedThemeHistory",joinedString);}};this.AddToUserSelectedThemeHistory=function(themeID){if(this.userSelectedThemeHistory==undefined){this.userSelectedThemeHistory=[themeID];}else{var index=this.userSelectedThemeHistory.indexOf(themeID);if(index>0){this.userSelectedThemeHistory.splice(index,1);}if(index!=0){this.userSelectedThemeHistory.unshift(themeID);}if(this.userSelectedThemeHistory.length>10){this.userSelectedThemeHistory.splice(10);}}};this.GetSystemTheme=function(){if(window.matchMedia&&window.matchMedia('(prefers-color-scheme: dark)').matches){return 1;}else{return 0;}};this.OnSystemThemeChange=function(event){if(this.userSelectedThemeID!=undefined&&this.userSelectedThemeID.startsWith("Auto:")){this.SetCurrentTheme(this.userSelectedThemeID,false);}};};var NDThemeSwitcher=new function(){this.Start=function(){this.switcherClickEventHandler=NDThemeSwitcher.OnSwitcherClick.bind(NDThemeSwitcher);this.keyDownEventHandler=NDThemeSwitcher.OnKeyDown.bind(NDThemeSwitcher);this.domSwitcher=document.getElementById("NDThemeSwitcher");var domSwitcherLink=document.createElement("a");domSwitcherLink.addEventListener("click",this.switcherClickEventHandler);this.domSwitcher.appendChild(domSwitcherLink);this.domMenu=document.createElement("div");this.domMenu.id="NDThemeSwitcherMenu";this.domMenu.style.display="none";this.domMenu.style.position="fixed";document.body.appendChild(this.domMenu);};this.IsNeeded=function(){return(NDThemes.availableThemes!=undefined&&NDThemes.availableThemes.length>1);};this.UpdateVisibility=function(){var themeSwitcher=document.getElementById("NDThemeSwitcher");var wasVisible=(themeSwitcher.style.display=="block");var shouldBeVisible=this.IsNeeded();if(!wasVisible&&shouldBeVisible){themeSwitcher.style.display="block";return true;}else if(wasVisible&&!shouldBeVisible){themeSwitcher.style.display="none";return true;}else{return false;}};this.OpenMenu=function(){if(!this.MenuIsOpen()){this.BuildMenu();this.domMenu.style.visibility="hidden";this.domMenu.style.display="block";this.PositionMenu();this.domMenu.style.visibility="visible";this.domSwitcher.classList.add("Active");window.addEventListener("keydown",this.keyDownEventHandler);}};this.CloseMenu=function(){if(this.MenuIsOpen()){this.domMenu.style.display="none";this.domSwitcher.classList.remove("Active");window.removeEventListener("keydown",this.keyDownEventHandler);}};this.MenuIsOpen=function(){return(this.domMenu!=undefined&&this.domMenu.style.display=="block");};this.BuildMenu=function(){var html="";if(NDThemes.availableThemes!=undefined){for(var i=0;i<NDThemes.availableThemes.length;i++){var theme=NDThemes.availableThemes[i];var safeThemeID=theme[1].replace(/[:\/]/g,"");html+="<a class=\"TSEntry TSEntry_"+safeThemeID+"Theme\"";if(theme[1]==NDThemes.userSelectedThemeID){html+=" id=\"TSSelectedEntry\"";}html+=" href=\"javascript:NDThemeSwitcher.OnMenuEntryClick('"+theme[1]+"');\">"+"<div class=\"TSEntryIcon\"></div>"+"<div class=\"TSEntryName\">"+theme[0]+"</div>"+"</a>";}}this.domMenu.innerHTML=html;};this.PositionMenu=function(){var x=this.domSwitcher.offsetLeft;var y=this.domSwitcher.offsetTop+this.domSwitcher.offsetHeight+5;var entryIcons=this.domMenu.getElementsByClassName("TSEntryIcon");if(entryIcons!=undefined&&entryIcons.length>=1){var entryIcon=entryIcons[0];x-=entryIcon.offsetLeft+this.domMenu.clientLeft;}this.domMenu.style.left=x+"px";this.domMenu.style.top=y+"px";};this.OnSwitcherClick=function(event){if(this.MenuIsOpen()){this.CloseMenu();}else{this.OpenMenu();}event.preventDefault();};this.OnMenuEntryClick=function(themeID){NDThemes.SetCurrentTheme(themeID,true);this.CloseMenu();};this.OnKeyDown=function(event){if(event.keyCode==27){if(this.MenuIsOpen()){this.CloseMenu();event.preventDefault();}}};this.OnUpdateLayout=function(){if(this.domMenu!=undefined){this.PositionMenu();}};};