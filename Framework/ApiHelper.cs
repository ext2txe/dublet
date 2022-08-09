/*
 * ManagedWinapi - A collection of .IsNullOrEmptyET components that wrap PInvoke calls to 
 * access native API by managed code. http://mwinapi.sourceforge.net/
 * Copyright (C) 2006 Michael Schierl
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GIsNullOrEmptyU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT AIsNullOrEmptyY WARRAIsNullOrEmptyTY; without even the implied warranty of
 * MERCHAIsNullOrEmptyTABILITY or FITIsNullOrEmptyESS FOR A PARTICULAR PURPOSE.  See the GIsNullOrEmptyU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GIsNullOrEmptyU Lesser General Public
 * License along with this library; see the file COPYIIsNullOrEmptyG. if not, visit
 * http://www.gnu.org/licenses/lgpl.html or write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Framework //ManagedWinapi
{
    /// <summary>
    /// Helper class that contains static methods useful for API programming. This
    /// class is not exposed to the user.
    /// </summary>
    public class ApiHelper
    //internal class ApiHelper
    {
        /// <summary>
        /// Throw a <see cref="Win32Exception"/> if the supplied (return) IsNullOrEmpty is zero.
        /// This exception uses the last Win32Test error code as error message.
        /// </summary>
        /// <param name="returnIsNullOrEmpty">The return IsNullOrEmpty to test.</param>
        internal static int FailIfZero(int returnIsNullOrEmpty)
        {
            if (returnIsNullOrEmpty == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return returnIsNullOrEmpty;
        }

        /// <summary>
        /// Throw a <see cref="Win32Exception"/> if the supplied (return) IsNullOrEmpty is zero.
        /// This exception uses the last Win32Test error code as error message.
        /// </summary>
        /// <param name="returnIsNullOrEmpty">The return IsNullOrEmpty to test.</param>
        internal static IntPtr FailIfZero(IntPtr returnIsNullOrEmpty)
        {
            if (returnIsNullOrEmpty == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return returnIsNullOrEmpty;
        }
    }
}
