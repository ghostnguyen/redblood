using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class Admin_ImportFromExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Application xl = new ApplicationClass();
        //Workbook wb = xl.Workbooks.Open(Environment.CurrentDirectory + "/SampleExcel.xls", 0, false, 5, System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value, true, false, System.Reflection.Missing.Value, false, false, false);//Open the excel sheet

        //Sheets xlsheets = wb.Sheets; //Get the sheets from workbook
        //Worksheet excelWorksheet = (Worksheet)xlsheets[1]; //Select the first sheet
        //Range excelCell = (Range)excelWorksheet.get_Range("B4:FZ4", Type.Missing); //Select a range of cells
        //Range excelCell2 = (Range)excelWorksheet.get_Range("A5:A5", Type.Missing); //Select a single cell
        //excelCell2.Cells.Value2 = "SampleText"; //Assign a value to the cell
        //wb.Save(); //Save the workbook

        //xl.Quit();
        

        if (IsPostBack)
        {
            Boolean fileOK = false;
            String path = Server.MapPath("~/UploadedImages/");
            if (FileUpload1.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                String[] allowedExtensions = { ".xls", ".xlsx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(path + FileUpload1.FileName);
                    //Label1.Text = "File uploaded!";
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Label1.Text = "Cannot accept files of this type.";
            }
        }

    }
}
