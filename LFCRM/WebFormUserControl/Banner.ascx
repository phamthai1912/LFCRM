<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="LFCRM.Menu" %>

		<div class="banner">
			<div id="topBanner">
			</div>
		</div>

        <div id="menu_user" runat="server" visible="false">
            <div id="nav">
			    <ul class="menu lgray slide" id="menu">
				    <li class="home"><a href=""><img src="../Image/home.png"></a></li>
                    <li><a href="">Bug Statistic</a></li>
                    <li><a href="">DTO/PTO</a></li>
                    <li><a href="">Device/Chip</a></li>
                    <li><a href="">Feedbacks</a></li>
                    <li><a href="">Non-work Hours</a></li>
			    </ul>
		    </div>
        </div>

        <div id="menu_admin" runat="server" visible="false">
            <div id="nav">
			    <ul class="menu lgray slide" id="menu">
				    <li class="home"><a href=""><img src="../Image/home.png"></a></li>
                    <li><a href="">Resource Management</a></li>
                    <li><a href="/AdminPage/ResourceAllocation">Resource Alocation</a></li>
                    <li><a href="/AdminPage/TitleManager">Title Management</a></li>
                    <li><a href="">Performance Tracking</a></li>
                    <%--<li><a href="">PTO/DTO</a></li>
                    <li><a href="">Device/Chip</a></li>
                    <li><a href="">Feedbacks</a></li>
                    <li><a href="">Data Analyzing</a></li>
                    <li><a href="">Non-work Hours</a></li>--%>
			    </ul>
		    </div>
        </div>
        
		<div class="layouts-icon">
            <div class="ctn">
                Welcome <asp:Label ID="lbl_fullname" runat="server" Text="lbl_fullname"></asp:Label>
                <ul>
                    <li class="contact"><a href=""></a></li>
                    <li class="logout">
                        <asp:ImageButton ID="btn_logout" runat="server" Height="22px" ImageUrl="../Image/logout.png" Width="25px" OnClick="btn_logout_Click"/>
                    </li>
                </ul>
            </div>
		</div>