<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="LFCRM.Menu" %>

		<div class="banner"></div>

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

        <div id="menu_admin" class="menu" runat="server" visible="false">
            <div id='cssmenu'>
		    <ul>
		       <li><a href='#'><img src="../Image/home.png"></a></li>
		       <li class='active has-sub'><a href='#'><span>Resource Management</span></a>
			      <ul>
				     <li class='has-sub'><a href='/AdminPage/ResourceAllocation'><span>Resource Allocation</span></a>
					    <ul>
					       <li><a href='#'><span>Sub Product</span></a></li>
					       <li class='last'><a href='#'><span>Sub Product</span></a></li>
					    </ul>
				     </li>
				     <li class='has-sub'><a href='#'><span>Product 2</span></a>
					    <ul>
					       <li><a href='#'><span>Sub Product</span></a></li>
					       <li class='last'><a href='#'><span>Sub Product</span></a></li>
					    </ul>
				     </li>
			      </ul>
		       </li>
		       <li><a href='/AdminPage/TitleManager'><span>Title Management</span></a></li>
		       <li class='last'><a href='#'><span>Performance Tracking</span></a></li>
                <%--<li><a href="">PTO/DTO</a></li>
                    <li><a href="">Device/Chip</a></li>
                    <li><a href="">Feedbacks</a></li>
                    <li><a href="">Data Analyzing</a></li>
                    <li><a href="">Non-work Hours</a></li>--%>
		    </ul>
		    </div>
        </div>
        
        <div class="welcome">
            Welcome <asp:Label ID="lbl_fullname" runat="server" Text="lbl_fullname"></asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" Height="22px" ImageUrl="../Image/logout.png" Width="25px" OnClick="btn_logout_Click"/>
        </div>