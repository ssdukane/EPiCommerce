var OxO7806=["nodeName","INPUT","TEXTAREA","BUTTON","IMG","SELECT","TABLE","position","style","absolute","relative","|H1|H2|H3|H4|H5|H6|P|PRE|LI|TD|DIV|BLOCKQUOTE|DT|DD|TABLE|HR|IMG|","|","body","document","allanchors","anchor_name","editor","window","name","value","[[ValidName]]","options","length","anchors","OPTION","text","#","images","className","cetempAnchor","anchorname"]; function Element_IsBlockControl(element){var name=element[OxO7806[0x0]];if(name==OxO7806[0x1]){return true;} ;if(name==OxO7806[0x2]){return true;} ;if(name==OxO7806[0x3]){return true;} ;if(name==OxO7806[0x4]){return true;} ;if(name==OxO7806[0x5]){return true;} ;if(name==OxO7806[0x6]){return true;} ;var Ox10f=element[OxO7806[0x8]][OxO7806[0x7]];if(Ox10f==OxO7806[0x9]||Ox10f==OxO7806[0xa]){return true;} ;return false;}  ; function Element_CUtil_IsBlock(Ox2e6){var Ox2e7=OxO7806[0xb];return (Ox2e6!=null)&&(Ox2e7.indexOf(OxO7806[0xc]+Ox2e6[OxO7806[0x0]]+OxO7806[0xc])!=-0x1);}  ; function Window_SelectElement(Ox1ae,element){if(Browser_UseIESelection()){if(Element_IsBlockControl(element)){var Ox2f=Ox1ae[OxO7806[0xe]][OxO7806[0xd]].createControlRange(); Ox2f.add(element) ; Ox2f.select() ;} else {var Ox19b=Ox1ae[OxO7806[0xe]][OxO7806[0xd]].createTextRange(); Ox19b.moveToElementText(element) ; Ox19b.select() ;} ;} else {var Ox19b=Ox1ae[OxO7806[0xe]].createRange();try{ Ox19b.selectNode(element) ;} catch(x){ Ox19b.selectNodeContents(element) ;} ;var Ox128=Ox1ae.getSelection(); Ox128.removeAllRanges() ; Ox128.addRange(Ox19b) ;} ;}  ;var allanchors=Window_GetElement(window,OxO7806[0xf],true);var anchor_name=Window_GetElement(window,OxO7806[0x10],true);var obj=Window_GetDialogArguments(window);var editor=obj[OxO7806[0x11]];var editwin=obj[OxO7806[0x12]];var editdoc=obj[OxO7806[0xe]];var name=obj[OxO7806[0x13]]; function insert_link(){var Ox2ec=anchor_name[OxO7806[0x14]];var Ox2ed=/[^a-z\d]/i;if(Ox2ed.test(Ox2ec)){ alert(OxO7806[0x15]) ;} else { Window_SetDialogReturnValue(window,Ox2ec) ; Window_CloseDialog(window) ;} ;}  ; function updateList(){while(allanchors[OxO7806[0x16]][OxO7806[0x17]]!=0x0){ allanchors[OxO7806[0x16]].remove(allanchors.options(0x0)) ;} ;if(Browser_IsWinIE()){for(var i=0x0;i<editdoc[OxO7806[0x18]][OxO7806[0x17]];i++){var Ox2ef=document.createElement(OxO7806[0x19]); Ox2ef[OxO7806[0x1a]]=OxO7806[0x1b]+editdoc[OxO7806[0x18]][i][OxO7806[0x13]] ; Ox2ef[OxO7806[0x14]]=editdoc[OxO7806[0x18]][i][OxO7806[0x13]] ; allanchors[OxO7806[0x16]].add(Ox2ef) ;} ;} else {var Ox2f0=editdoc[OxO7806[0x1c]];if(Ox2f0){for(var j=0x0;j<Ox2f0[OxO7806[0x17]];j++){var img=Ox2f0[j];if(img[OxO7806[0x1d]]==OxO7806[0x1e]){var Ox2ef=document.createElement(OxO7806[0x19]); Ox2ef[OxO7806[0x1a]]=OxO7806[0x1b]+img.getAttribute(OxO7806[0x1f]) ; Ox2ef[OxO7806[0x14]]=img.getAttribute(OxO7806[0x1f]) ; allanchors[OxO7806[0x16]].add(Ox2ef) ;} ;} ;} ;} ;}  ; function selectAnchor(Ox2f2){ editor.FocusDocument() ;for(var i=0x0;i<editdoc[OxO7806[0x18]][OxO7806[0x17]];i++){if(editdoc[OxO7806[0x18]][i][OxO7806[0x13]]==Ox2f2){ anchor_name[OxO7806[0x14]]=Ox2f2 ; Window_SelectElement(editwin,editdoc[OxO7806[0x18]][i]) ;} ;} ;}  ;if(name){ anchor_name[OxO7806[0x14]]=name ;} ; updateList() ;