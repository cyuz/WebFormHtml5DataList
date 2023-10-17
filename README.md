# WebFormHtml5DataList
Extender to Show HTML5 DataList to webform

## Usage
1. register DLL
   - sepcify at page start
   > [!IMPORTANT]
   > use correct namespace for proper Control.
   > * Extender:WebFormHtml5DataList.Extender
   > * Validator:WebFormHtml5DataList.Validator
   > * WebFormHtml5DataList.GridView:WebFormHtml5DataList.GridView
   > * MaskInput:WebFormHtml5DataList.MaskInput
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
2. DataListExtender
   1. add the extender and set TargetControlID to TextBox control 
      ```
      <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
      <CustomExtenderInPage:HTML5DataListControlExtender ID="HTML5DataListControlExtenderFromDataSource" TargetControlID="TextBox1" runat="server">                
      </CustomExtenderInPage:HTML5DataListControlExtender>
      ```
   2. use it like ListControl 
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
   3. Validator
      - just sepcify the dependency
      ```
      <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
      <CustomValidatorInPage:CustomRequiredValidator ID="CV1_Required" runat="server" ControlToValidate="TextBox1" Display="Dynamic" ></CustomValidatorInPage:CustomRequiredValidator>
      <CustomValidatorInPage:CustomRegexValidator ID="CV1_Regex" runat="server" ControlToValidate="TextBox1" DependentValidatorControl="CV1_Required" RegexFormat="^\d{1,3}$" isplay="Dynamic"></CustomValidatorInPage:CustomRegexValidator>
      <CustomValidatorInPage:CustomDependepntValidator ID="CV1_CV" runat="server" ControlToValidate="TextBox1" DependentValidatorControl="CV1_Required, CV1_Regex" OnServerValidate="CV1_CV_ServerValidate" isplay="Dynamic"></CustomValidatorInPage:CustomDependepntValidator>
      ```
   5. MaskInput
      - must import jquery and jquery.mask[https://igorescobar.github.io/jQuery-Mask-Plugin/]
        ```
        <script src="js/jquery-3.5.1.min.js"></script>
        <script src="js/jquery.mask-1.4.1.min.js"></script>        
        ```
      - just specify format
        ```
        Decimal(5,0)
        <br />
        <CustomMaskInputInPage:DecimalTextBoxControl ID="DecimalTextBox1" runat="server" IntPart="5"></CustomMaskInputInPage:DecimalTextBoxControl>
        <br />
        ROCDate
        <br />
        <CustomMaskInputInPage:ROCDateTextBoxControl ID="ROCDateTextBox1" runat="server" ></CustomMaskInputInPage:ROCDateTextBoxControl>
        <br />
        ROCMonth
        <br />
        <CustomMaskInputInPage:ROCMonthTextBoxControl ID="ROCMonthTextBox1" runat="server" ></CustomMaskInputInPage:ROCMonthTextBoxControl>
        <br />
        English + digit only
        <br />
        <CustomMaskInputInPage:AnyCharTextBoxControl ID="AnyCharTextBox1" runat="server" UTF8="false" MaxLength="20" ></CustomMaskInputInPage:AnyCharTextBoxControl>        
        ```
   6. CustomEncodeBoundField
      - just use CustomEncodeBoundField to replace BoundField and set HeaderHtmlEncode=true
      ```
      <CustomGridViewInPage:CustomHeaderEncodeBoundField DataField="Val" HeaderHtmlEncode="false" HeaderText="two<br/>line"></CustomGridViewInPage:CustomHeaderEncodeBoundField>
      ```