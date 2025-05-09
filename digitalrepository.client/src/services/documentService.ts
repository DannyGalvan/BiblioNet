import { GenericFormData, toFormData } from "axios";
import { api } from "../config/axios/interceptors";
import { DocumentRequest } from "../pages/documents/LoadPage";
import { ApiResponse } from "../types/ApiResponse";
import { DocumentResponse } from "../types/DocumentResponse";

export const loadDocumetToServer = async (
  form: DocumentRequest,
): Promise<ApiResponse<any>> => {
  const response = await api.post<
    any,
    ApiResponse<DocumentResponse>,
    GenericFormData
  >("Document", toFormData(form), {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  return response;
};

export const getDocuments = async (
  filter?: string,
  page = 1,
  pageSize = 10,
): Promise<ApiResponse<DocumentResponse[]>> => {
  let response: ApiResponse<DocumentResponse[]>;

  console.log("filter", filter);

  if (filter) {
    response = await api.get<any, ApiResponse<DocumentResponse[]>>(
      `Document?filters=${filter}&page=${page}&pageSize=${pageSize}`,
    );
  } else {
    response = await api.get<object, ApiResponse<DocumentResponse[]>>(
      `Document?page=${page}&pageSize=${pageSize}`,
    );
  }

  return response;
};
