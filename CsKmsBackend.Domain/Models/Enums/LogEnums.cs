namespace CsKmsBackend.Domain.Models.Enums
{
	public enum ActionType
	{
		Create,
		Update,
		Delete,
		RequestAccess,
		ApproveAccess,
		DenyAccess
	}

	public enum EntityType
	{
		Post,
		AccessRequest
	}
}
