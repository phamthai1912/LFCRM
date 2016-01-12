<%@ Control Language="C#" AutoEventWireup="true" CodeFile="frmExchangeRate.ascx.cs" Inherits="frmExchangeRate" %>
<br /><br /><br /><br />
<div style="padding: 20px 20px 40px 20px">
        <table cellpadding="0" cellspacing="0" width="150px">
            <tr>
                <td>
                    <asp:Label id="lbl_tygia" runat="server"></asp:Label>
                </td>
            </tr>
            <asp:Repeater ID="rptTiGia" runat="server">
                <ItemTemplate>
                    <tr class="idtr" style="background: #fff; color: Black; height: 20px;">
                        <td>
                            <%#Eval("CurrencyCode") %>
                        </td>
                        <td>
                            <%#Eval("Buy") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="idtr2" style="background: #fafafa; color: Black; height: 20px;">
                          <td>
                            <%#Eval("CurrencyCode") %>
                        </td>
                        <td>
                            <%#Eval("Buy") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
        </table>
    </div>