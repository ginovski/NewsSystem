<%@ Page Title="Edit Articles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditArticles.aspx.cs" Inherits="NewsSystem.Admin.EditArticle" %>

<%@ Import Namespace="NewsSystem.Extensions" %>
<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="ListViewEditArticle" ItemType="NewsSystem.Models.Article" SelectMethod="ListViewEditArticle_GetData" UpdateMethod="ListViewEditArticle_UpdateItem" DataKeyNames="ID" InsertMethod="ListViewEditArticle_InsertItem" InsertItemPosition="None" DeleteMethod="ListViewEditArticle_DeleteItem">
        <LayoutTemplate>
            <div class="row">
                <asp:LinkButton Text="Sort by Title" runat="server" CommandName="Sort" CommandArgument="Title" CssClass="col-md-2 btn btn-default" />
                <asp:LinkButton Text="Sort by Date" runat="server" CommandName="Sort" CommandArgument="DateCreated" CssClass="col-md-2 btn btn-default" />
                <asp:LinkButton Text="Sort by Category" runat="server" CssClass="col-md-2 btn btn-default" />
                <asp:LinkButton Text="Sort by Likes" runat="server"  CssClass="col-md-2 btn btn-default" />
                <br />
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder" />
                <asp:LinkButton runat="server" ID="LinkButtonInsert" CssClass="btn btn-info pull-right" OnClick="LinkButtonInsert_Click">Insert new Article</asp:LinkButton>
              
                <asp:DataPager runat="server" PageSize="5" PagedControlID="ListViewEditArticle">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="false" ShowPreviousPageButton="true" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowNextPageButton="true" ShowPreviousPageButton="false" />
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <h3><%#: Item.Title %>
                    <asp:LinkButton Text="Edit" runat="server" CommandName="Edit" CssClass="btn btn-info" />
                    <asp:LinkButton Text="Delete" runat="server" CommandName="Delete" CssClass="btn btn-danger" />
                </h3>
                <p>Category: <%#: Item.Category.Name %></p>
                <p>
                    <asp:Literal Text='<%#: string.Format("{0}{1}", Item.Content.Truncate(300), Item.Content.Length > 300 ? "..." : "")%>' runat="server" />
                </p>
                <p>Likes count: <%#: Item.Likes.Count() %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="row">
                <h3>
                    <asp:TextBox runat="server" ID="TextBoxEditArticleTitle" Text="<%#: BindItem.Title %>" />
                    <asp:LinkButton Text="Save" runat="server" CommandName="Update" CssClass="btn btn-success" />
                    <asp:LinkButton Text="Cancel" runat="server" CommandName="Cancel" CssClass="btn btn-danger" />
                </h3>
                <p>
                    <asp:DropDownList runat="server" ID="ddlEditArticleCategory" ItemType="NewsSystem.Models.Category" SelectMethod="GetAllCategories" DataTextField="Name" DataValueField="ID" SelectedValue="<%#: BindItem.CategoryID %>"></asp:DropDownList>
                </p>
                <p>
                    <asp:TextBox runat="server" ID="TextBoxEditArticleContent" Text="<%#: BindItem.Content %>" TextMode="MultiLine" Rows="10" Columns="100" />
                </p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="row">
                <h3>Title: 
                    <asp:TextBox runat="server" ID="TextBoxInsertArticleTitle" CssClass="form-control" Text="<%#: BindItem.Title %>" />
                </h3>
                <p>
                    Category:  
                    <asp:DropDownList runat="server" ID="ddlInsertArticleCategory" ItemType="NewsSystem.Models.Category" SelectMethod="GetAllCategories" DataTextField="Name" DataValueField="ID" SelectedValue="<%#: BindItem.CategoryID %>"></asp:DropDownList>
                </p>
                <p>
                    Content: 
			                   
                    <asp:TextBox runat="server" ID="TextBoxInsertArticleContent" CssClass="form-control" Text="<%#: BindItem.Content %>" TextMode="MultiLine" Rows="10" Columns="25" />
                </p>
                <div>
                    <asp:LinkButton Text="Insert" runat="server" CssClass="btn btn-success" CommandName="Insert" />
                    <asp:LinkButton Text="Cancel" runat="server" ID="LinkButtonCancel" CssClass="btn btn-danger" CommandName="Cancel" OnClick="LinkButtonCancel_Click" />
                </div>
            </div>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>
