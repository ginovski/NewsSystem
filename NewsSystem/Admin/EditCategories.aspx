<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCategories.aspx.cs" Inherits="NewsSystem.Admin.EditCategories" %>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="ListViewEditCategories" ItemType="NewsSystem.Models.Category" SelectMethod="ListViewEditCategories_GetData" UpdateMethod="ListViewEditCategories_UpdateItem" DataKeyNames="ID" InsertItemPosition="LastItem" InsertMethod="ListViewEditCategories_InsertItem" DeleteMethod="ListViewEditCategories_DeleteItem">
        <LayoutTemplate>
            <div class="row">
                <asp:LinkButton Text="Category Name" runat="server" CssClass="btn btn-default" CommandName="Sort" CommandArgument="Name" />
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
            <asp:DataPager runat="server" PageSize="5" PagedControlID="ListViewEditCategories">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="false" ShowPreviousPageButton="true" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowNextPageButton="true" ShowPreviousPageButton="false" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-3"><%#: Item.Name %></div>
                <asp:LinkButton Text="Edit" runat="server" CssClass="btn btn-info" CommandName="Edit" />
                <asp:LinkButton Text="Delete" runat="server" CssClass="btn btn-danger" CommandName="Delete" />
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="TextBoxEditItem" Text="<%#: BindItem.Name %>" CssClass="form-control" />
                </div>
                <asp:LinkButton Text="Save" runat="server" CssClass="btn btn-success" CommandName="Update" />
                <asp:LinkButton Text="Cancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel" />
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="TextBoxInsertItem" Text="<%#: BindItem.Name %>" CssClass="form-control" />
                </div>
                <asp:LinkButton Text="Save" runat="server" CssClass="btn btn-info" CommandName="Insert" />
                <asp:LinkButton Text="Cancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel" />
            </div>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>
