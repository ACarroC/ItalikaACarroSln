# MusicSchools API

Este proyecto es una API para gestionar Escuelas de Música, Profesores y Alumnos, con relaciones entre ellos.

---

## 1. Base de datos

La base de datos se llama `MusicSchoolsDb` y tiene la siguiente estructura principal:

- Tabla `MusicSchools`: almacena las escuelas
- Tabla `Teachers`: almacena profesores
- Tabla `Students`: almacena alumnos
- Tabla `StudentTeacher`: relación muchos a muchos entre alumnos y profesores
- Stored Procedures (`sp_ManageSchool`, `sp_ManageTeacher`, `sp_ManageStudent`) para manejar el CRUD

---

## 2. Backup y restauración de base de datos SQL Server

### 2.1 Crear backup (respaldo)

Puedes crear un backup con el siguiente comando en SQL Server Management Studio (SSMS) o en una consola SQL:

```sql
BACKUP DATABASE MusicSchoolsDb
TO DISK = 'C:\Backups\MusicSchoolsDb.bak'
WITH FORMAT;
