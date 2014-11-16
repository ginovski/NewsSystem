<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="NewsSystem.News" %>
<%@ Import Namespace="NewsSystem.Extensions" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Title %></h1>
    <asp:Repeater runat="server" ID="RepeaterNews" ItemType="NewsSystem.Models.Article" SelectMethod="RepeaterNews_GetData">
        <ItemTemplate>
            <div class="row">
                <h3><a href='<%#: string.Format("/ViewArticle?id={0}", Item.ID) %>'><%#: Item.Title %></a> <small><%#: Item.Category.Name %></small></h3>
                <p><%#: string.Format("{0}{1}", Item.Content.Truncate(300), Item.Content.Length > 300 ? "..." : "" ) %></p>
                <p>Likes: <%#: Item.Likes.Count() %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:LoginView runat="server">
        <LoggedInTemplate>
            <asp:ListView runat="server" ID="ListViewCategories" GroupItemCount="2" ItemType="NewsSystem.Models.Category" SelectMethod="ListViewCategories_GetData">
                <GroupTemplate>
                    <div class="row">
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                    </div>
                </GroupTemplate>
                <ItemTemplate>
                    <div class="col-md-6">
                        <h3><%#: Item.Name %></h3>
                        <asp:ListView runat="server" ItemType="NewsSystem.Models.Article" DataSource="<%# Item.Articles.OrderByDescending(a => a.DateCreated).Take(3)%>">
                            <LayoutTemplate>
                                <ul>
                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <a href='<%#: string.Format("/ViewArticle?id={0}", Item.ID) %>'><strong><%#: Item.Title %></strong> <i>by <%#: Item.Author.UserName %></i></a>
                                </li>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                No articles
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
