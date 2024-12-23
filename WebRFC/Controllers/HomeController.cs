using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;

namespace WebRFC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult IrFormRFC()
        {
            return View("VistaFormRFC");
        } 

        public ActionResult IrEliminar()
        {

            return View("VistaEliminar");
        }

        public ActionResult GenerarRFC(E_RFC objRFC)
        {
            try
            {
                //Crear el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                negocio.GenerarRFC(objRFC);

                TempData["mensaje"] = $"Se generó el rfc con éxito";

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View("VistaMostrarRFC",objRFC);
        }
        
        public ActionResult MostrarBD()
        {
            //Crear una lista vacia
            List<E_RFC> objRFC = new List<E_RFC>();
            try
            {
                //Crear el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                objRFC = negocio.ObtenerRFCs();
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("VistaDbRFC",objRFC);
        }

        public ActionResult IrEditar(int idRFC)
        {
            try
            {
                //Creamos el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                //Obtengo el objeto pelicula de la capa de negocio a través del idPelicula
                E_RFC objRFC = negocio.ObternerRFCporId(idRFC);

                //Pasamos el objeto pelicula a la vista
                return View("VistaEditar", objRFC);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("VistaDbRFC");
            }
        }

        public ActionResult Editar(E_RFC objRFC)
        {
            try
            {
                //Creamos el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                //Mandar a llamar el método Editar de la capa de negocio
                negocio.Editar(objRFC);

                TempData["mensaje"] = $"El rfc se actualizó correctamente";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("MostrarBD");
        }

        public ActionResult Eliminar(int idRFC)
        {
            try
            {
                //Creamos el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                //Mandar a llamar el método ELiminar de la capa de negocio
                negocio.Eliminar(idRFC);

                TempData["mensaje"] = $"El rfc se eliminó correctamente";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("MostrarBD");
        }

        public ActionResult AvisoEliminar(int idRFC)
        {
            //Creamos el objeto de la capa de negocio
            N_RFC negocio = new N_RFC();
            //Obtengo el objeto producto de la capa de negocio a través del idProducto
            E_RFC objRFC = negocio.ObternerRFCporId(idRFC);
            return View("VistaEliminar", objRFC);
        }
        public ActionResult BuscarRFC(string textoBusqueda)
        {
            List<E_RFC> datos = new List<E_RFC>();
            try
            {
                //Creamos el objeto de la capa de negocio
                N_RFC negocio = new N_RFC();

                //Obtengo la lista de datos de personas con rfc
                datos = negocio.BuscarRFC(textoBusqueda);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("VistaDbRFC", datos);
        }
    }
}