import { Button } from "@heroui/button";
import { addToast } from "@heroui/toast";
import { downloadPdf } from "../../services/documentService";
import { DocumentResponse } from "../../types/DocumentResponse";
import { TableColumnWithFilters } from "../../types/TableColumnWithFilters";
import { Icon } from "../icons/Icon";

export const DownloadDocumentColumns: TableColumnWithFilters<DocumentResponse>[] =
  [
    {
      id: "id",
      name: "Id",
      selector: (data) => data.id ?? "",
      sortable: true,
      maxWidth: "150px",
      omit: true,
    },
    {
      id: "documentNumber",
      name: "Número de Documento",
      selector: (data) => data.documentNumber ?? "",
      sortable: true,
      wrap: true,
      omit: false,
      hasFilter: true,
      filterField: (value) => (value ? `DocumentNumber:like:${value}` : ""),
    },
    {
      id: "author",
      name: "Propietario",
      selector: (data) => data.author ?? "",
      sortable: true,
      wrap: true,
      omit: false,
      hasFilter: true,
      filterField: (value) => (value ? `Author:like:${value}` : ""),
    },
    {
      id: "createdAt",
      name: "Creado",
      selector: (data) => data.createdAt ?? "",
      sortable: true,
      maxWidth: "160px",
      omit: true,
    },
    {
      id: "updatedAt",
      name: "Actualizado",
      selector: (data) => data.updatedAt ?? "",
      sortable: true,
      maxWidth: "160px",
      omit: true,
    },
    {
      id: "actions",
      name: "Acciones",
      center: true,
      cell: (data) => (
        <Button
          isIconOnly
          color="danger"
          onPress={async () => {
            const response = await downloadPdf(data.id);

            if (response) {
              addToast({
                title: "Success",
                description: "Documento descargado correctamente",
                icon: "bi bi-exclamation-triangle-fill",
                color: "success",
                timeout: 5000,
              });
            } else {
              addToast({
                title: "Error",
                description: "Error al descargar el documento",
                icon: "bi bi-exclamation-triangle-fill",
                color: "danger",
                timeout: 5000,
              });
            }
          }}
        >
          <Icon name="bi bi-file-earmark-pdf" color="white" size={20} />
        </Button>
      ),
      sortable: false,
      maxWidth: "160px",
      omit: false,
    },
  ];
