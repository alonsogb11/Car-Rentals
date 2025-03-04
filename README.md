## _Descripción_ ✏️
Proyecto final de la materia **Desarrollo de software con tecnología propietaria I (ISO-605)** de la Universidad APEC, en el cual trabajaremos todo el cuatrimestre. Se trata de una **Aplicación Web** de un concecionario de autos de renta, **CarRentals**.

## _Autores_ 💡
**Alonso Genao | A00114295 | Back-End, WebUI y Front-End**

**Axel Grullón | A00111051 | Documentación**

**Ruby Rosario | A00105745 | Front-End**

**Carl Weasman | A00111246 | Front-End**

## _Instalción_ 📦
Simplemente se clona el repositorio en Visual Studio Community 2022 y se ejecuta el proyecto.

## _Información_ ℹ️
El proyecto está hecho con el enfoque **Code-First** utilizando **.NET 8.0** y el **Model-View-Controller de ASP.NET Core** con **Visual Studio Community 2022**. Para el login y registro utilizamos **Identity Framework**.

- ### **Lenguajes utilizados**:
<div align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dot-net/dot-net-original.svg" height="40" alt="dot-net logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="40" alt="csharp logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-original.svg" height="40" alt="html5 logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-original.svg" height="40" alt="css3 logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/javascript/javascript-original.svg" height="40" alt="javascript logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" height="40" alt="microsoftsqlserver logo"  />
</div>

- ### **Tecnologías utilizadas**:
<div align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/visualstudio/visualstudio-plain.svg" height="40" alt="visualstudio logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" height="40" alt="dotnetcore logo"  />
  <img width="12" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/github/github-original.svg" height="40" alt="dotnetcore logo" />
</div>

## _Requisitos generales_ 📄
- Gestión de Tipos de Vehículos
    - Identificador
    - Descripción
    - Estado

- Gestión de Marcas
    - Identificador
    - Descripción
    - Estado

- Gestión de Modelos
    - Identificador
    - Identificador de Marca (fk)
    - Descripción
    - Estado

- Gestión de Tipos de Combustible
    - Identificador
    - Descripción
    - Estado

- Gestión de Vehículos
    - Identificador
    - Descripción
    - No. Chasis
    - No. Motor
    - No. Placa
    - Tipo Vehiculo
    - Marca
    - Modelo
    - Tipo Combustible 
    - Estado

- Gestión de Clientes
    - Identificador
    - Nombre
    - Cedula
    - No. Tarjeta CR
    - Límite de Credito
    - Tipo Persona (Física / Jurídica) 
    - Estado

- Gestión de Empleados (quienes asisten al cliente en la renta)
    - Identificador
    - Nombre
    - Cédula
    - Tanda labor
    - Porciento Comision
    - Fecha Ingreso 
    - Estado

- Proceso de Inspección (previo a renta)
    - Identificador Transacción
    - Vehículo
    - Identificador Cliente
    - Tiene Ralladuras
    - Cantidad Combustible (1/4, ½, ¾, lleno) 
    - Tiene Goma respuesta
    - Tiene Gato
    - Tiene roturas cristal
    - Estado gomas (un check para cada una)
    - Fecha
    - Empleado inspección 
    - Estado 

- Proceso de Renta y Devolución de vehículos
    - No. Renta
    - Empleado
    - Vehículo
    - Cliente
    - Fecha Renta
    - Fecha Devolución
    - Monto x Día
    - Cantidad de días
    - Comentario
    - Estado
