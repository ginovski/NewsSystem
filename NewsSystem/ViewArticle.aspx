<%@ Page Title="View Article" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewArticle.aspx.cs" Inherits="NewsSystem.ViewArticle" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView runat="server" ID="FormViewArticleDetails" ItemType="NewsSystem.Models.Article" SelectMethod="FormViewArticleDetails_GetItem">
        <ItemTemplate>
            <table cellspacing="0" style="border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td colspan="2" runat="server">
                            <div>
                                <div class="like-control col-md-1">
                                    <div>Likes</div>
                                    <asp:LoginView runat="server">
                                        <LoggedInTemplate>
                                            <div>
                                                <a class="btn btn-default glyphicon glyphicon-chevron-up" href="#"></a>
                                        </LoggedInTemplate>
                                    </asp:LoginView>
                                    <span class="like-value"><%#:Item.Likes.Count() %></span>
                                    <asp:LoginView runat="server">
                                        <LoggedInTemplate>
                                            <a class="btn btn-default glyphicon glyphicon-chevron-down" href="#"></a>
                                        </LoggedInTemplate>
                                    </asp:LoginView>
                                </div>
                            </div>
                            </div>
                        </td>
                        <td>
                            <h2><%#: Item.Title %> <small>Category: <%#: Item.Category.Name %></small></h2>
                            <p><%#: Item.Content %></p>
                            <p>
                                <span>Author: <%#: Item.Author.UserName %></span>
                                <span class="pull-right"><%#: Item.DateCreated %></span>
                            </p>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
