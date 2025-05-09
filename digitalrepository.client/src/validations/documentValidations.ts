// export interface DocumentRequest {
//     author: string;
//     documentNumber: string;
//     elaborationDate: string;
//     file: File | null;
//   }

import { z } from "zod";
import { invalid_type_error, required_error } from "../config/constants";

export const documentShema = z.object({
  author: z
    .string({ invalid_type_error, required_error })
    .min(1, { message: "El autor es requerido" }),
  documentNumber: z
    .string({ invalid_type_error, required_error })
    .min(1, { message: "El número de documento es requerido" }),
  elaborationDate: z
    .string({ invalid_type_error, required_error })
    .min(1, { message: "La fecha de elaboración es requerida" }),
  file: z
    .instanceof(File, { message: "El archivo debe ser un pdf" })
    .refine((file) => file.size < 5000000, {
      message: "El archivo debe pesar menos de 5MB",
    })
    .refine((file) => file.type === "application/pdf", {
      message: "El archivo debe ser un pdf",
    })
    .refine((file) => file.name.endsWith(".pdf"), {
      message: "El archivo debe tener una extensión .pdf",
    }),
});
