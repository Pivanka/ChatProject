import { USER } from "./authorization.models";

export interface MESSAGE {
  id: number;
  text: string;
  createdAt: Date;
  updatedAt?: Date;
  userId: number;
  groupId: number;
  deletedForUser: boolean;
  parentMessageId?: number;
  parentMessage?: string;
  userName: string;
}

export interface MESSAGE_TO_ADD{
  text: string;
  createdAt: Date;
  userEmail: string;
  groupId: number;
  parentMessageId?: number;
}

export interface MESSAGE_TO_UPDATE{
  id: number;
  text: string;
}

export interface GROUP {
  id?: number;
  groupName: string;
  users?: Array<USER>
}
