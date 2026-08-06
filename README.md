# 📋 Sistema de Control de Folios por Departamento

&gt; Plataforma web para la centralización, control de acceso departamental y gestión de oficios institucionales.

---

## 🎯 Objetivo

Centralizar el control de folios y oficios institucionales —actualmente administrados en hojas de Excel— dentro de una base de datos SQL Server, garantizando que cada usuario solo acceda a la información de su departamento y permitiendo solicitudes de acceso temporal puntuales entre departamentos.

---

## ✨ Características Principales

- 🔐 **Control de acceso departamental** — cada usuario visualiza únicamente los folios de su propio departamento.
- 📝 **Solicitudes de acceso temporal** — un usuario puede solicitar acceso puntual a un folio de otro departamento, sujeto a aprobación y límite de tiempo.
- ✅ **Aprobación por responsable** — el jefe del departamento dueño del folio aprueba o rechaza la solicitud.
- ⏰ **Expiración automática** — los accesos temporales se revocan automáticamente al vencer su fecha límite.
- 📊 **Auditoría completa** — registro de todas las solicitudes, aprobaciones, rechazos, previsualizaciones y descargas.
- 📁 **Importación desde Excel** — migración masiva de folios históricos mediante archivos `.xlsx`.
- 🖨️ **Previsualización en PDF** — conversión automática de documentos Word a PDF para visualización segura en el navegador.
- 📄 **Generación de oficios** — numeración correlativa por departamento/año con reserva temporal de números.

---

## 🏗️ Stack Tecnológico

| Capa | Tecnología | Justificación |
|------|-----------|---------------|
| **Backend** | ASP.NET Core (Web API / MVC) | Estándar actual en C# para aplicaciones web empresariales; seguro, escalable y con soporte a largo plazo. |
| **Frontend** | Blazor Server | Interfaz en C# compartiendo lógica con el backend, reduciendo la necesidad de JavaScript. |
| **Base de datos** | SQL Server | Motor relacional robusto, requisito definido del proyecto. |
| **ORM** | Entity Framework Core | Mapeo de clases C# a tablas SQL con migraciones automáticas. |
| **Autenticación** | ASP.NET Core Identity | Manejo de usuarios, roles y claims por departamento. |
| **Tareas programadas** | Hangfire | Ejecución en segundo plano de revocación de accesos vencidos y limpieza de reservas. |
| **Importación Excel** | ClosedXML | Lectura de archivos `.xlsx` sin costo de licencia. |
| **Conversión PDF** | LibreOffice (headless) | Generación automática de previsualizaciones PDF desde documentos Word. |
| **Visualización PDF** | PDF.js | Visualización de documentos directamente en el navegador. |

---

## 🏛️ Arquitectura General
       CAPA DE PRESENTACIÓN                        │
│              Interfaz web (Blazor Server)                    │
│     Consulta de folios · Solicitudes · Administración        │
├─────────────────────────────────────────────────────────────┤
│                 CAPA DE LÓGICA DE NEGOCIO                    │
│    Servicios C# · Validación de permisos · Reglas de         │
│    expiración · Generación de números · Conversión PDF       │
├─────────────────────────────────────────────────────────────┤
│                    CAPA DE DATOS                             │
│              SQL Server (Entity Framework Core)              │
│   Metadatos de documentos · Control de accesos · Auditoría   │
├─────────────────────────────────────────────────────────────┤
│              REPOSITORIO DE ARCHIVOS FÍSICOS                 │
│     C:\ArchivosMunicipalidad\ (o ruta configurada)          │
│   .docx originales · .pdf previsualizaciones · Plantillas    │
└─────────────────────────────────────────────────────────────┘
plain

### Organización del Almacenamiento Físico
RepositorioOficios/
└── Oficios/
└── {Departamento}/
└── {Año}/
└── {IdOficio}/
├── original.docx
└── preview.pdf
plain

---

## 🗄️ Modelo de Datos

### Entidades Principales

| Tabla | Descripción |
|-------|-------------|
| **Departamentos** | Catálogo de departamentos de la organización. |
| **Usuarios** | Usuarios del sistema; cada uno pertenece a un departamento y tiene un rol asignado. |
| **Folios / Control_Oficios** | Registro administrativo de cada oficio: número consecutivo, código institucional, destinatario, referencia, etc. |
| **Archivos** | Metadatos de documentos físicos: nombre, ruta, tamaño, hash SHA-256, previsualización PDF. |
| **SolicitudesAcceso** | Control de quién solicitó acceso a qué folio, quién lo aprobó y hasta cuándo es válido. |
| **Accesos_Temporales** | Permisos vigentes concedidos con fecha de inicio y expiración. |
| **Auditoria** | Bitácora de consultas y acciones sobre folios ajenos al departamento. |
| **Oficios_En_Progreso** | Reservas temporales de números de oficio (anti-duplicados). |
| **Plantillas** | Plantillas oficiales por departamento para generación de nuevos oficios. |

### Estados de una Solicitud

- 🟡 **Pendiente** — en espera de revisión del responsable del departamento.
- 🟢 **Aprobada** — acceso otorgado con fecha de expiración definida.
- 🔴 **Rechazada** — acceso denegado con observación.
- ⚪ **Expirada** — acceso revocado automáticamente al vencer el plazo.

---

## 🔄 Flujos del Sistema

### 1. Creación de un Oficio
Usuario abre "Crear Oficio"
→ sp_Oficio_ObtenerSiguienteNumero (muestra: "Tu oficio será el #47")
→ sp_Reserva_Crear (reserva el #47 por 10 minutos)
Usuario guarda el oficio
→ sp_Oficio_Crear (inserta con #47, genera código MUPA-104-ASLE-47-2026)
→ sp_Archivo_Registrar (guarda metadatos del .docx)
→ Conversión a PDF + sp_Archivo_MarcarPreviewGenerado
plain

### 2. Acceso a Oficio de Otro Departamento
Usuario intenta ver oficio ajeno
→ sp_Acceso_Validar (¿tengo permiso?)
→ SÍ: muestra el archivo / previsualización
→ NO: habilita "Solicitar Acceso"
→ sp_Solicitud_Crear (estado: Pendiente)
→ Notificación al jefe del departamento dueño
Jefe aprueba solicitud
→ sp_Solicitud_Aprobar (define fecha límite)
→ sp_Acceso_Crear (acceso temporal vigente)
→ Usuario puede acceder hasta la fecha de expiración
plain

### 3. Tareas Automáticas (Hangfire)
Cada 5 minutos:
→ sp_Reserva_LimpiarVencidas (libera números abandonados)
Cada hora:
→ sp_Acceso_RevocarVencidos (cierra accesos expirados)
plain

---

## 🛡️ Seguridad

- **Autenticación** mediante ASP.NET Core Identity.
- **Autorización por rol:** Usuario, Responsable de Departamento, Administrador.
- **Filtro obligatorio por departamento** en la capa de servicios (no solo en la interfaz).
- **Verificación de accesos temporales vigentes** antes de exponer cualquier folio ajeno.
- **Hash SHA-256** para verificar integridad de archivos.
- **Registro de IP** en cada acción de auditoría.

---

## 📦 Módulos del Sistema

| Módulo | Descripción |
|--------|-------------|
| **Gestión de Oficios** | Creación, edición, anulación y búsqueda de oficios con numeración correlativa. |
| **Solicitudes de Acceso** | Flujo completo de solicitud, aprobación, rechazo y seguimiento. |
| **Accesos Temporales** | Visualización de permisos vigentes y revocación automática. |
| **Importación Excel** | Carga masiva de folios históricos con validación de duplicados y departamentos. |
| **Plantillas** | Administración de plantillas oficiales por departamento. |
| **Auditoría** | Consulta de trazabilidad completa por oficio o por usuario. |
| **Reportes** | Estadísticas por departamento, solicitudes pendientes y accesos temporales activos. |

---

## 📂 Estructura del Proyecto
GestorOficios/
├── Components/
│   ├── Layout/
│   ├── Pages/
│   │   ├── Folios/
│   │   ├── Solicitudes/
│   │   ├── Admin/
│   │   └── Dashboard/
│   └── Shared/
├── Models/
│   ├── Entities/
│   ├── DTOs/
│   └── Enums/
├── Data/
│   └── ApplicationDbContext.cs
├── Services/
│   ├── Interfaces/
│   └── Implementations/
├── Repositories/
│   ├── Interfaces/
│   └── Implementations/
├── Storage/
│   ├── Interfaces/
│   └── FileStorageService.cs
├── Security/
│   ├── Authentication/
│   └── Authorization/
├── Jobs/
│   └── ExpiracionAccesoJob.cs
├── Validators/
├── wwwroot/
├── appsettings.json
└── Program.cs
plain

---

## 🚀 Fases del Proyecto

| Fase | Entregable | Duración Est. |
|------|-----------|---------------|
| 1. Análisis y diseño | Modelo de datos definitivo, mockups de pantallas | 1–2 semanas |
| 2. Base de datos y backend base | SQL Server, entidades, autenticación | 2 semanas |
| 3. Módulo de importación Excel | Carga y validación de folios existentes | 1 semana |
| 4. Consulta de folios por departamento | Pantallas y filtros por defecto | 1–2 semanas |
| 5. Flujo de solicitudes de acceso | Solicitud, aprobación, expiración automática | 2 semanas |
| 6. Auditoría y ajustes finales | Bitácora, pruebas, capacitación | 1 semana |

**Duración total estimada:** 8 a 10 semanas.

---

## 🛠️ Requisitos Previos

- [.NET 8.0+](https://dotnet.microsoft.com/)
- [SQL Server 2019+](https://www.microsoft.com/sql-server)
- [LibreOffice](https://www.libreoffice.org/) (para conversión de documentos a PDF)
- Node.js (solo si se requieren herramientas de build adicionales)

---

## ⚙️ Configuración Inicial

```bash
# 1. Clonar el repositorio
git clone <url-del-repositorio>
cd GestorOficios

# 2. Restaurar paquetes NuGet
dotnet restore

# 3. Configurar cadena de conexión en appsettings.json
#    "ConnectionStrings": {
#      "DefaultConnection": "Server=...;Database=GestorOficios;..."
#    }

# 4. Ejecutar migraciones de base de datos
dotnet ef database update

# 5. Configurar ruta de almacenamiento en appsettings.json
#    "Storage": {
#      "RootPath": "C:\\ArchivosMunicipalidad"
#    }

# 6. Ejecutar la aplicación
dotnet run
📄 Licencia
Proyecto interno institucional. Todos los derechos reservados.
Documentación generada a partir de la propuesta técnica y documentación de arquitectura del Sistema de Control de Folios.
