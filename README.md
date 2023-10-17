# WebFormHtml5DataList
Extender to Show HTML5 DataList to webform

## Usage
1. register DLL 
   - sepcify at page start 
   ```
   <%@ Register TagPrefix="CustomExtenderInPage" Namespace="WebFormHtml5DataList.Extender" Assembly= "WebFormHtml5DataList" %>
   ```
   - specify once at web.cofnig 
   ```
   <system.web>
     <compilation debug="true" targetFramework="4.8" />
     <httpRuntime targetFramework="4.8" />
     <pages>
       ...
       <controls>
         <add tagPrefix="CustomExtenderInWebConfig" assembly="WebFormHtml5DataList" namespace="WebFormHtml5DataList.Extender"/>
       </controls>
     </pages>
   </system.web>
   ```
2. add the extender and set TargetControlID to TextBox control 
   ```
   <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
   <CustomExtenderInPage:HTML5DataListControlExtender ID="HTML5DataListControlExtenderFromDataSource" TargetControlID="TextBox1" runat="server">                
   </CustomExtenderInPage:HTML5DataListControlExtender>
   ```
3. use it like ListControl 
   - from datasource 
   ```
   DataTable dt = new DataTable();
   dt.Columns.Add(new DataColumn("OptionText"));
   
   DataRow dr = dt.NewRow();
   dr["OptionText"] = "option1";
   dt.Rows.Add(dr);
   
   dr = dt.NewRow();
   dr["OptionText"] = "option2";
   dt.Rows.Add(dr);
   
   this.HTML5DataListControlExtenderFromDataSource.DataSource = dt;
   this.HTML5DataListControlExtenderFromDataSource.DataValueField = "OptionText";
   this.HTML5DataListControlExtenderFromDataSource.DataBind();
   ```
   - set items from bode behind 
   ```
   this.HTML5DataListControlExtenderListItem.Items.Add(new ListItem("option1"));
   this.HTML5DataListControlExteanderListItem.Items.Add(new ListItem("option2"));
   ```
   - or set items in aspx page 
   ```
   <CustomExtenderInWebConfig:HTML5DataListControlExtender ID="HTML5DataListControlExtenderListItemInPage" TargetControlID="TextBox3" runat="server">
     <asp:ListItem Value="123"></asp:ListItem>
     <asp:ListItem Value="456"></asp:ListItem>
   </CustomExtenderInWebConfig:HTML5DataListControlExtender>
   ```