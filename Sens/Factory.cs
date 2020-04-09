using Sens.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sens {

    public static class Factory {

        public static ICalculator CreateCalculator() {

            return new Calculator();
        }

        public static ICalculator2D CreateCalculator2D() {

            return new Calculator2D();
        }

        public static IWriteToCFG CreateCFGWriter(ICalculator calculator) {

            return new WriteToCFG(CreateCalculator());
        }
    }
}
