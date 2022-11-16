// import { Ticket } from "./ticket"

import { Member } from "./member"

export interface Project{
  id: number
  name: string
  description: string
  tickets: Ticket[]
}

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
