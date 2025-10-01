# Arquitectura Hexagonal DDD - Módulo de Usuarios

Este proyecto implementa un módulo completo de gestión de usuarios siguiendo los principios de **Arquitectura Hexagonal** y **Domain-Driven Design (DDD)** en .NET 6.

## 🏗️ Arquitectura

El proyecto está estructurado siguiendo la Arquitectura Hexagonal con las siguientes capas:

### 📁 Estructura del Proyecto

```
ArquitecturaHexagonalDDD/
├── App/
│   ├── Domain/                    # Capa de Dominio
│   │   ├── Shared/               # Elementos compartidos del dominio
│   │   │   ├── AggregateRoot.cs  # Clase base para agregados
│   │   │   ├── ValueObject.cs    # Clase base para objetos de valor
│   │   │   └── IDomainEvent.cs   # Interfaz para eventos de dominio
│   │   └── Users/                # Agregado de Usuario
│   │       ├── Entity/           # Entidades del dominio
│   │       ├── ValueObject/      # Objetos de valor
│   │       ├── Event/            # Eventos de dominio
│   │       ├── Exception/        # Excepciones del dominio
│   │       ├── Repository/       # Interfaces de repositorio
│   │       ├── Service/          # Servicios de dominio
│   │       └── Usuario.cs        # Entidad principal Usuario
│   ├── Application/              # Capa de Aplicación
│   │   └── Users/
│   │       ├── Dto/              # Objetos de transferencia de datos
│   │       │   ├── Command/      # Comandos (CQRS)
│   │       │   ├── Query/        # Consultas (CQRS)
│   │       │   └── Response/     # Respuestas
│   │       ├── Port/             # Puertos (interfaces)
│   │       │   ├── In/           # Puertos de entrada (casos de uso)
│   │       │   └── Out/          # Puertos de salida (infraestructura)
│   │       ├── Service/          # Servicios de aplicación
│   │       └── Mapper/           # Mapeo de objetos
│   └── Infrastructure/           # Capa de Infraestructura
│       ├── Adapters/             # Adaptadores
│       │   ├── DatabaseEntityFramework/  # Adaptador de base de datos
│       │   └── Security/          # Adaptadores de seguridad
│       └── EntrypointRest/       # Punto de entrada REST API
│           ├── Users/            # Controladores de usuarios
│           └── CommonErrorHandler/ # Manejo de errores
└── postman/                      # Colección de pruebas API
```

## 🚀 Funcionalidades Implementadas

### ✅ CRUD Completo de Usuarios
- **Crear Usuario**: Registro de nuevos usuarios con validación
- **Leer Usuario**: Obtener usuario por ID
- **Actualizar Usuario**: Modificar datos del usuario
- **Eliminar Usuario**: Eliminación de usuarios
- **Listar Usuarios**: Lista paginada con filtros

### 🔐 Autenticación y Autorización
- **Login**: Autenticación con JWT
- **Logout**: Cierre de sesión
- **Cambio de Contraseña**: Actualización segura de contraseñas
- **Recuperación de Contraseña**: Flujo de reset de contraseña

### 🛡️ Seguridad
- **Hash de Contraseñas**: BCrypt para encriptación
- **Validación de Fortaleza**: Políticas de contraseñas seguras
- **JWT Tokens**: Autenticación basada en tokens
- **Autorización por Roles**: Control de acceso basado en roles

## 🛠️ Tecnologías Utilizadas

- **.NET 6**: Framework principal
- **Entity Framework Core 6.0**: ORM para base de datos
- **PostgreSQL**: Base de datos relacional
- **JWT Bearer**: Autenticación con tokens
- **BCrypt.Net-Next**: Encriptación de contraseñas
- **AutoMapper**: Mapeo de objetos
- **FluentValidation**: Validación de datos

## 📋 Prerrequisitos

- .NET 6 SDK
- PostgreSQL 13+
- Visual Studio 2022 o VS Code
- Postman (para pruebas de API)

## 🚀 Configuración e Instalación

### 1. Clonar el Repositorio
```bash
git clone <repository-url>
cd ArquitecturaHexagonalDDD
```

### 2. Restaurar Dependencias
```bash
dotnet restore
```

### 3. Configurar Base de Datos
Editar `appsettings.json` (ejemplo para PostgreSQL local):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ArquitecturaHexagonalDDD;Username=postgres;Password=postgres"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "ArquitecturaHexagonalDDD",
    "Audience": "ArquitecturaHexagonalDDD-Users",
    "ExpirationHours": 24
  }
}
```

### 4. Aplicar Migraciones
```bash
dotnet ef database update
```

### 5. Ejecutar la Aplicación
```bash
dotnet run
```

La API estará disponible en:
- **HTTPS**: `https://localhost:7000`
- **HTTP**: `http://localhost:5000`

## 📚 Documentación de la API

### Endpoints Disponibles

#### 🔐 Autenticación
- `POST /api/users/login` - Iniciar sesión
- `POST /api/users/logout` - Cerrar sesión

#### 👥 Gestión de Usuarios
- `POST /api/users` - Crear usuario
- `GET /api/users/{id}` - Obtener usuario por ID
- `PUT /api/users/{id}` - Actualizar usuario
- `DELETE /api/users/{id}` - Eliminar usuario
- `GET /api/users` - Listar usuarios (paginado)

#### 🔑 Gestión de Contraseñas
- `POST /api/users/change-password` - Cambiar contraseña
- `POST /api/users/request-password-reset` - Solicitar reset
- `POST /api/users/reset-password` - Resetear contraseña

### Ejemplo de Uso

#### Crear Usuario
```bash
curl -X POST "https://localhost:7000/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "testuser",
    "email": "test@example.com",
    "password": "Test123!",
    "role": "User"
  }'
```

#### Login
```bash
curl -X POST "https://localhost:7000/api/users/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test123!"
  }'
```

## 🧪 Pruebas con Postman

Se incluye una colección completa de Postman en `postman/ArquitecturaHexagonalDDD-Users-API.postman_collection.json` con:

- **Variables de entorno**: `baseUrl`, `authToken`, `userId`
- **Casos de prueba**: Creación, autenticación, CRUD completo
- **Escenarios de error**: Validación de datos, credenciales inválidas
- **Flujos completos**: Registro → Login → Operaciones → Logout

### Importar en Postman
1. Abrir Postman
2. Importar → File → Seleccionar `ArquitecturaHexagonalDDD-Users-API.postman_collection.json`
3. Configurar variables de entorno si es necesario

## 🏛️ Principios DDD Implementados

### 📦 Agregados
- **Usuario**: Agregado raíz con invariantes de negocio

### 🎯 Objetos de Valor
- **UserId**: Identificador único
- **UserName**: Nombre de usuario
- **Email**: Dirección de correo
- **PasswordHash**: Hash de contraseña
- **Role**: Rol del usuario

### 📢 Eventos de Dominio
- **UserRegisteredEvent**: Usuario registrado
- **UserPasswordChangedEvent**: Contraseña cambiada
- **UserDeactivatedEvent**: Usuario desactivado
- **UserReactivatedEvent**: Usuario reactivado
- **UserRenamedEvent**: Usuario renombrado
- **UserRoleAssignedEvent**: Rol asignado

### ⚠️ Excepciones de Dominio
- **UserNotFoundException**: Usuario no encontrado
- **EmailAlreadyExistsException**: Email duplicado
- **InvalidPasswordException**: Contraseña inválida
- **InvalidRoleException**: Rol inválido
- **InvalidUserNameException**: Nombre de usuario inválido
- **UserAlreadyActiveException**: Usuario ya activo
- **UserAlreadyInactiveException**: Usuario ya inactivo

## 🔧 Patrones de Arquitectura

### 🏗️ Arquitectura Hexagonal
- **Puertos**: Interfaces que definen contratos
- **Adaptadores**: Implementaciones concretas
- **Inversión de Dependencias**: El dominio no depende de infraestructura

### 📋 CQRS (Command Query Responsibility Segregation)
- **Commands**: Operaciones que modifican estado
- **Queries**: Operaciones de consulta
- **Handlers**: Procesadores de comandos y consultas

### 🎯 Repository Pattern
- **IUsuarioRepository**: Contrato de persistencia
- **EntityFrameworkUserRepository**: Implementación con EF Core

### 🔄 Unit of Work
- **IUnitOfWorkPort**: Contrato de transacciones
- **EntityFrameworkUnitOfWork**: Implementación con EF Core

## 📈 Próximas Mejoras

- [ ] Implementar caché con Redis
- [ ] Agregar logging estructurado
- [ ] Implementar métricas y monitoreo
- [ ] Agregar tests unitarios e integración
- [ ] Implementar rate limiting
- [ ] Agregar documentación con Swagger/OpenAPI
- [ ] Implementar auditoría de cambios
- [ ] Agregar notificaciones por email

## 🤝 Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 👥 Autores

- **Tu Nombre** - *Trabajo inicial* - [TuGitHub](https://github.com/tu-usuario)

## 🙏 Agradecimientos

- Comunidad de .NET
- Documentación de Arquitectura Hexagonal
- Principios de Domain-Driven Design
