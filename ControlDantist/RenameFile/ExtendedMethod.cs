﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace ControlDantist.RenameFile
{
    public static class ExtendedMethod
    {
        public static void Rename(this FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + " + " + newName);
        }
    }
}
