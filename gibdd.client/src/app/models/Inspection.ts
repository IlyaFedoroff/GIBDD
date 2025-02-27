
export interface AddInspection {
  carId: number;
  ownerId: number;
  officerId: number;
  inspectionDate: string;
  result: string;
}

export interface Inspection {
  inspectionId?: number; // Необязательный для новых записей
  inspectionDate: string; // Формат ISO (например, "2024-12-03T12:00:00Z")
  result: string;
  carId: number;
  officerId: number;
  ownerId: number;
}
