using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_RFC
    {
        public void GenerarRFC(E_RFC objRFC)
        {
            //creamos un objeto de la capa de datos
            D_RFC datos = new D_RFC();

            objRFC.RFC = GuardarRFC(objRFC);
            //Mandamos el metodo GenerarRFC a la capa de datos
            datos.GenerarRFC(objRFC);
        }

        public string GuardarRFC(E_RFC objRFC)
        {
            //Validaciones


            //declaramos variables string para crear el rfc
            string paterno = ValidarENIE(objRFC.ApellidoPat);
            string parteapellidopat = ValidarPartesApellidos(paterno).Substring(0, 2).ToUpper();
            //Si no hay apellido, la tercera posición será X
            string materno = string.IsNullOrEmpty(objRFC.ApellidoMat) ? "X" : ValidarENIE(objRFC.ApellidoMat).Substring(0, 1).ToUpper();
            string parteapellidomat = materno;
            // Validar y procesar los campos (remover acentos y convertir a mayúsculas)
            string nombres = ProcesarTexto(objRFC.Nombre);
            string partenombre = ValidarNombresCompuestos(nombres).Substring(0, 1).ToUpper();
            // Construir los primeros 4 caracteres del RFC para validar que no esté una palabra altisonante
            string palabra = parteapellidopat + parteapellidomat + partenombre;
            palabra = ValidarPalabrasAltisonantes(palabra);

            string anio = objRFC.FechaNacimiento.Year.ToString().Substring(2, 2);
            string mes = objRFC.FechaNacimiento.Month.ToString("00");
            string dia = objRFC.FechaNacimiento.Day.ToString("00");




            objRFC.RFC = palabra + anio + mes + dia;

            return objRFC.RFC;
        }
        public List<E_RFC> ObtenerRFCs()
        {
            //Crear un objeto de la capa de datos
            D_RFC datos = new D_RFC();

            //Mando a llamar el metodo para obtener la lista de rfc
            return datos.ObtenerRFCs();
        }

        public E_RFC ObternerRFCporId(int idRFC)
        {
            //Crear un objeto de la capa de datos
            D_RFC datos = new D_RFC();

            //Mando a llamar el metodo para obtener la lista de rfc
            return datos.ObtenerRFCporId(idRFC);
        }
        
        public List<E_RFC> BuscarRFC(string texto)
        {
            //Crear un objeto de la capa de datos
            D_RFC datos = new D_RFC();

            //Mando a llamar el metodo para obtener la lista de datos de rfc
            return datos.BuscarRFC(texto);
        }

        public void Editar(E_RFC objRFC)
        {
            //Crear un objeto de la capa de datos
            D_RFC datos = new D_RFC();
            objRFC.RFC = GuardarRFC(objRFC);
            //Mando a llamar el metodo para obtener la lista de rfc
            datos.EditarRFC(objRFC);
        }

        public void Eliminar(int idRFC)
        {
            //Crear un objeto de la capa de datos
            D_RFC datos = new D_RFC();

            //Mando a llamar el metodo para obtener la lista de rfc
            datos.Eliminar(idRFC);
        }

        //VALIDACIONES DE LOS NOMBRES PARA GENERAR EL RFC
        public string ValidarENIE(string nombreoapellido)
        {
            if (nombreoapellido.StartsWith("Ñ", StringComparison.OrdinalIgnoreCase))
            {
                return "X" + nombreoapellido.Substring(1).ToUpper();
            }

            return nombreoapellido.ToUpper();
        }
        public string ValidarNombresCompuestos(string nombrescomp)
        {
            string[] nombres = nombrescomp.Split(' ');
            if (nombres.Length > 1 && (nombres[0].ToUpper() == "MARIA" || nombres[0].ToUpper() == "MA."
                || nombres[0].ToUpper() == "JOSE" || nombres[0].ToUpper() == "J."))
            {
                return nombres[1].ToUpper();
            }
            return nombres[0].ToUpper();
        }
        public string ValidarPartesApellidos(string apellido)
        {
            char primeraLetra = apellido[0];
            string vocales = "AEIOU";
            for (int posicion = 1; posicion < apellido.Length; posicion++)
            {
                if (vocales.Contains(apellido[posicion]))
                {
                    return primeraLetra.ToString().ToUpper() + apellido[posicion].ToString().ToUpper();
                }
            }
            return apellido.Substring(0, 1).ToUpper() + "X";
        }
        //Función para remover acentos y convertir a mayusculas
        public string ProcesarTexto(string input)
        {
            // Convertir a mayúsculas y remover acentos
            string texto = input.ToUpper();
            texto = Regex.Replace(texto, "[ÁÀÂÄ]", "A");
            texto = Regex.Replace(texto, "[ÉÈÊË]", "E");
            texto = Regex.Replace(texto, "[ÍÌÎÏ]", "I");
            texto = Regex.Replace(texto, "[ÓÒÔÖ]", "O");
            texto = Regex.Replace(texto, "[ÚÙÛÜ]", "U");
            texto = Regex.Replace(texto, "Ñ", "N");
            return texto;
        }
        // Lista de palabras altisonantes
        private static readonly Dictionary<string, string> palabrasAltisonantes = new Dictionary<string, string>
        {
            {"BUEI", "BUEX"}, {"BUEY", "BUEX"}, {"CACA", "CACX"}, {"CACO", "CACX"},
            {"CAGA", "CAGX"}, {"CAGO", "CAGX"}, {"CAKA", "CAKX"}, {"CAKO", "CAKX"},
            {"COGE", "COGX"}, {"COJA", "COJX"}, {"COJE", "COJX"}, {"COJI", "COJX"},
            {"COJO", "COJX"}, {"CULO", "CULX"}, {"FETO", "FETX"}, {"GUEY", "GUEX"},
            {"JOTO", "JOTX"}, {"KACA", "KACX"}, {"KACO", "KACX"}, {"KAGA", "KAGX"},
            {"KAGO", "KAGX"}, {"KOGE", "KOGX"}, {"KOJO", "KOJX"}, {"KAKA", "KAKX"},
            {"KULO", "KULX"}, {"MAME", "MAMX"}, {"MAMO", "MAMX"}, {"MEAR", "MEAX"},
            {"MEAS", "MEAX"}, {"MEON", "MEOX"}, {"MION", "MIOX"}, {"MOCO", "MOCX"},
            {"MULA", "MULX"}, {"PEDA", "PEDX"}, {"PEDO", "PEDX"}, {"PENE", "PENX"},
            {"PUTA", "PUTX"}, {"PUTO", "PUTX"}, {"QULO", "QULX"}, {"RATA", "RATX"}
        };
        private static string ValidarPalabrasAltisonantes(string primerosCuatro)
        {
            // Si los primeros cuatro caracteres forman una palabra altisonante, corregirla
            if (palabrasAltisonantes.ContainsKey(primerosCuatro))
            {
                return palabrasAltisonantes[primerosCuatro];
            }

            // Si no es altisonante, devolver tal cual
            return primerosCuatro;
        }
    }
}
