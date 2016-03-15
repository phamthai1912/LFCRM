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
		       <li class='active has-sub'><a href='#'><span>Resources</span></a>
			      <ul>
                     <li><a href='/AdminPage/Resources'><span>Resources Management</span></a></li>
				     <li><a href='/AdminPage/ResourceAllocation'><span>Resource Allocation</span></a></li>
			      </ul>
		       </li>
		       <li class='active has-sub'><a href='#'><span>Titles/Categories</span></a>
                   <ul>
                       <li>
                           <a href="/AdminPage/TitleManager"><span>Title Management</span></a>
                       </li>
                       <li>
                           <a href="/AdminPage/Category"><span>Category Management</span></a>
                       </li>
                   </ul>
		       </li>
		       <li class='active has-sub'><a href='#'><span>Performance</span></a>
                    <ul>
                        <li>
                            <a href="/AdminPage/BugStatistic"><span>Fill Bug Statistic</span></a>
                        </li>
                        <li>
                            <a href="/AdminPage/PerformanceTracking"><span>Performance Tracking</span></a>
                        </li>
                    </ul>
		        </li>
                <li><a href="/AdminPage/BillingList"><span>Billing List</span></a>
                </li>
                <li class='active has-sub'>
                    <a href="#"><span>Profile</span></a>
                    <ul>
                        <li>
                            <a href="/AdminPage/ByTester"><span>Tester</span></a>
                        </li>
                        <li>
                            <a href="/AdminPage/ByTitle"><span>Title - TBD</span></a>
                        </li>
                    </ul>
                </li>
                <li>
                    <a href="/AdminPage/CoreTracking"><span>Core Tracking</span></a>
                </li>
		    </ul>
		    </div>
        </div>
        
        <div class="welcome">
            Welcome <asp:Label ID="lbl_fullname" runat="server" Text="lbl_fullname"></asp:Label>
            <asp:ImageButton ID="ImageButton1" runat="server" Height="22px" ImageUrl="../Image/logout.png" Width="25px" OnClick="btn_logout_Click"/>
        </div>