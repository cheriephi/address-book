using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AddressBook")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AddressBook")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("8e4e3f43-c40e-4a1d-a108-8c4e9389a9f1")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Expose types / methods with internalmodifier to the corresponding unit test project
// This is a work around to support testing on interfaces that are not designed with testing in mind
// See https://weblogs.asp.net/bhouse/using-internalsvisibleto-attribute-with-strong-named-assemblies
// Because this assembly is signed so it can be referenced by the web, the test assembly also has to be signed and referenced by its public key
[assembly: InternalsVisibleTo("AddressBookTest, PublicKey=0024000004800000940000000602000000240000525341310004000001000100ddfdfb84f40e232f14ed31f4bc5091a65beea19beb0e3cefc5da23468aae347bf107828118c8bdbe530b5ebf1baa5183d5261a7dce8a2660af0b57d00c5221fea9eb0102a597740e1ea615e84c3d34cb89675a737d084219ac5ad0e8d69331fe76bd4939b430d7ba017e901ba99b151db53f14d41e8e924cf01592ab32e244ce")]
