//Qy_ Board Command List
//    Command Number | Command Name              | Argument Used | Required Data Bytes   | Received Data Bytes
//                 0 | Null                      | No            | 0                     | 0
//                 1 | Read Status               | No            | 0                     | 1
//                 2 | Write to Digital Outputs  | No            | 1                     | 0
//                 3 | Read Digital Inputs       | No            | 0                     | 1
//                 4 | Write to Analog Output    | Yes           | 2                     | 0
//                 5 | Read Analog Inputs        | Yes           | 0                     | 2-8
//                 6 | Write to USART 1          | Yes           | 1-15                  | 0
//                 7 | Read USART 1 Buffer       | Yes           | 0                     | 1-15
//                 8 | Write to USART 2          | Yes           | 1-15                  | 0
//                 9 | Read USART 2 Buffer       | Yes           | 0                     | 1-15
//                10 | Write to SPI Port         | Yes           | 1-15                  | 0
//                11 | Read SPI Port Buffer      | Yes           | 0                     | 1-15
//                12 | I²C Transaction           | Yes           | 1-16                  | 1-16
//                13 | SD Card Transaction       | Yes           | X                     | X
//                14 | Write to Settings         | Yes           | 1-4                   | 0
//                15 | Read Settings             | No            | 0                     | 6
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBoardTesting
{
    internal class QBoard
    {
    }
}
