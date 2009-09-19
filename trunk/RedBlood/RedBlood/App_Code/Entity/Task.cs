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
        DeleteBackupPackRemain = 5,

        CountStore = 6,
        DeleteCountStore = 7,

        CountStoreRemain = 8,
        DeleteCountStoreRemain = 9


    }

    public Task()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
