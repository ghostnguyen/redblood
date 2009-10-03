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
        
        DoFinalizeStore = 10,

        BackupPackRemain = 12,
        DeleteBackupPackRemain = 13,

        CountPackTransaction = 14,
        DeleteCountPackTransaction = 15,

        CountPackRemain = 16,
        DeleteCountPackRemain = 17
    }

    public Task()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
