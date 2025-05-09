import { create } from "zustand";

import { ListFilter } from "../types/LIstFilter";

interface DocumentState {
  documentFilters: ListFilter;
  setDocumentFilters: (documentFilters: ListFilter) => void;
}

export const useDocumentStore = create<DocumentState>((set) => ({
  documentFilters: { filter: "", page: 1, pageSize: 10 },
  setDocumentFilters: (documentFilters) => set({ documentFilters }),
}));
