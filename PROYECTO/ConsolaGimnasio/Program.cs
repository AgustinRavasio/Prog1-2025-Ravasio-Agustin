
using LogicaDeNegocio;

var servicio = new GimnasioService();

while (true)
{
    Console.WriteLine("=== MENÚ GIMNASIO ===");
    Console.WriteLine("1. Registrar socio");
    Console.WriteLine("2. Registrar actividad grupal");
    Console.WriteLine("3. Registrar entrenamiento personalizado");
    Console.WriteLine("4. Inscribir socio a actividad");
    Console.WriteLine("5. Registrar asistencia");
    Console.WriteLine("6. Consultar asistencias de un socio");
    Console.WriteLine("7. Consultar socios por actividad");
    Console.WriteLine("8. Cancelar inscripción");
    Console.WriteLine("9. Salir");
    Console.Write("Seleccione una opción: ");
    var opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.Write("Documento: ");
                string doc = Console.ReadLine();
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Fecha nacimiento (yyyy-mm-dd): ");
                DateTime fnac = DateTime.Parse(Console.ReadLine());
                Console.Write("Nivel de membresía (Basico, Plus, Premium): ");
                NivelMembresia nivel = Enum.Parse<NivelMembresia>(Console.ReadLine(), true);
                var socio = new Socio
                {
                    Documento = doc,
                    NombreCompleto = nombre,
                    FechaNacimiento = fnac,
                    NivelMembresia = nivel,
                    FechaInscripcion = DateTime.Now
                };
                var res1 = servicio.RegistrarSocio(socio);
                Console.WriteLine(res1.Message);
                break;

            case "2":
                Console.Write("Código: ");
                string cod1 = Console.ReadLine();
                Console.Write("Nombre actividad: ");
                string nomAct1 = Console.ReadLine();
                Console.Write("Duración (min): ");
                int dur1 = int.Parse(Console.ReadLine());
                Console.Write("Nivel requerido: ");
                NivelMembresia nivel1 = Enum.Parse<NivelMembresia>(Console.ReadLine(), true);
                Console.Write("Profesor: ");
                string prof1 = Console.ReadLine();
                Console.Write("Cupo máximo: ");
                int cupo = int.Parse(Console.ReadLine());
                var clase = new ClaseGrupal
                {
                    Codigo = cod1,
                    Nombre = nomAct1,
                    DuracionMinutos = dur1,
                    NivelRequerido = nivel1,
                    Profesor = prof1,
                    CupoMaximo = cupo
                };
                var res2 = servicio.RegistrarActividad(clase);
                Console.WriteLine(res2.Message);
                break;

            case "3":
                Console.Write("Código: ");
                string cod2 = Console.ReadLine();
                Console.Write("Nombre actividad: ");
                string nomAct2 = Console.ReadLine();
                Console.Write("Duración (min): ");
                int dur2 = int.Parse(Console.ReadLine());
                Console.Write("Nivel requerido: ");
                NivelMembresia nivel2 = Enum.Parse<NivelMembresia>(Console.ReadLine(), true);
                Console.Write("Profesor: ");
                string prof2 = Console.ReadLine();
                Console.Write("Costo adicional: ");
                decimal costo = decimal.Parse(Console.ReadLine());
                Console.Write("Horario: ");
                string horario = Console.ReadLine();
                var ent = new EntrenamientoPersonalizado
                {
                    Codigo = cod2,
                    Nombre = nomAct2,
                    DuracionMinutos = dur2,
                    NivelRequerido = nivel2,
                    Profesor = prof2,
                    CostoAdicional = costo,
                    Horario = horario
                };
                var res3 = servicio.RegistrarActividad(ent);
                Console.WriteLine(res3.Message);
                break;

            case "4":
                Console.Write("Documento socio: ");
                string doci = Console.ReadLine();
                Console.Write("Código actividad: ");
                string codi = Console.ReadLine();
                var res4 = servicio.InscribirSocio(doci, codi, DateTime.Now);
                Console.WriteLine(res4.Message);
                break;

            case "5":
                Console.Write("Documento socio: ");
                string doca = Console.ReadLine();
                Console.Write("Código actividad: ");
                string coda = Console.ReadLine();
                var res5 = servicio.RegistrarAsistencia(doca, coda, DateTime.Now);
                Console.WriteLine(res5.Message);
                break;

            case "6":
                Console.Write("Documento socio: ");
                string docs = Console.ReadLine();
                Console.Write("Desde (yyyy-mm-dd): ");
                DateTime desde = DateTime.Parse(Console.ReadLine());
                Console.Write("Hasta (yyyy-mm-dd): ");
                DateTime hasta = DateTime.Parse(Console.ReadLine());
                var asistencias = servicio.ConsultarAsistencias(docs, desde, hasta);
                foreach (var a in asistencias)
                    Console.WriteLine($"{a.Fecha}: {a.NombreActividad}, {a.Profesor}, {a.Descripcion}");
                break;

            case "7":
                Console.Write("Código actividad: ");
                string coda2 = Console.ReadLine();
                var socios = servicio.ConsultarSociosPorActividad(coda2);
                foreach (var s in socios)
                    Console.WriteLine($"{s.NombreCompleto} ({s.Documento})");
                break;

            case "8":
                Console.Write("Documento socio: ");
                string docx = Console.ReadLine();
                Console.Write("Código actividad: ");
                string codx = Console.ReadLine();
                var res6 = servicio.CancelarInscripcion(docx, codx);
                Console.WriteLine(res6.Message);
                break;

            case "9":
                return;

            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }

    Console.WriteLine();
}
