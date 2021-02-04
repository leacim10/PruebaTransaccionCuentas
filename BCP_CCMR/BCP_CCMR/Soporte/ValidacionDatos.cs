using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BCP_CCMR.Soporte
{
    public class ValidacionDatos
    {
        private ValidationContext context;
        private List<ValidationResult> results;
        private bool valid;
        private string mensaje;

        public ValidacionDatos(object instace)
        {
            context = new ValidationContext(instace);
            results = new List<ValidationResult>();
            valid = Validator.TryValidateObject(instace, context, results);
        }
        public bool validacion()
        {
            if (valid == false)
            {
                foreach (ValidationResult item in results)
                {
                    mensaje = item.ErrorMessage + "\n";
                }
                System.Windows.Forms.MessageBox.Show(mensaje);
            }
            return valid;
        }
    }
}
