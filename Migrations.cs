using JetBrains.Annotations;
using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace Orchard.Mobile.Contrib {
    [UsedImplicitly]
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("DeviceGroupRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Name")
                    .Column<string>("Description")
                    .Column<string>("IconPath")
                    .Column<string>("SelectionRule")
                    .Column<string>("Theme")
                    .Column<bool>("Enabled")
                    .Column<int>("Position")
                    .Column<bool>("SwitcherEnabled")
                    .Column<string>("SwitcherText")
                    .Column<string>("SwitcherPosition")
                    .Column<string>("SwitcherZone")
                );

            ContentDefinitionManager.AlterTypeDefinition("DeviceGroup",
                cfg => cfg
                   .WithPart("DeviceGroupPart")
                );

            return 1;
        }
    }
}