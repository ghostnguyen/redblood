using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Task
/// </summary>
public class Task
{
    public enum TaskX
    {
        ScanExp = 1,
        CloseOrder = 2,
        LockEnterTestResult = 3,
        
        
        BackupPackRemain = 4,
        FinalizeStore = 5,

        DeleteOldDataForFinalizeStore = 6
    }

    public Task()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
