import { Member } from "./member"

export interface Ticket {
  id: number
  title: string
  created: Date
  assignedDeveloper: Member
  submitter: Member
  projectId: number
  description: string
  status: string
  priority: string
}
