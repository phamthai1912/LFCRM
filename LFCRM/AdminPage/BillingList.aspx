<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BillingList.aspx.cs" Inherits="LFCRM.AdminPage.BillingList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableCdn="True"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True" onkeydown = "return (event.keyCode!=13)">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #F0F0F0; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;"></div>
                    <div style="position: fixed; top: 40%; left: 40%; height:15%; width:15%; z-index: 100001;  background-color: #FFFFFF; background-image: url('../Image/loading.gif'); background-repeat: no-repeat; background-position:center;"></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        <div class="topright-grid">
                    <ul>
                        <li>
                            <asp:TextBox ID="txt_date" placeholder="Filter billing list by Date" runat="server" AutoPostBack="true" 
                                class="form-control" OnTextChanged="txt_date_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txt_date_CalendarExtender" runat="server" BehaviorID="txt_date_CalendarExtender" TargetControlID="txt_date" Format="MM/dd/yyyy" />
                        </li>
                    </ul>
        </div><br /><br />
            <h3><asp:Label ID="lb_status" runat="server"></asp:Label></h3>
            <hr />
            <br />                   
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            <br />
            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        <br /><br />
    </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
