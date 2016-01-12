<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmSpeaker.ascx.cs" Inherits="frmSpeaker" %>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<img src="Anh/Loa.gif" style="width: 210px; height: 148px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<img src="Anh/Loa.gif" style="width: 210px; height: 148px" />
               
<script type='text/javascript' src='player/jwplayer.js'></script> 
<div id='mediaspace'>This text will be replaced</div>
<script type='text/javascript'>
    jwplayer('mediaspace').setup({
    'flashplayer': 'player/player.swf',
    'file': 'Anh/quangcao.mp4',
    'autostart': 'true',
    'repeat': 'always',
    'controlbar': 'bottom',
    'width': '1',
    'height': '1'
    });
</script>